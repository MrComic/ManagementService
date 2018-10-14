import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import {EventEmitter, Inject, Injectable} from '@angular/core';
import { Router } from "@angular/router";
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { catchError, finalize, map } from "rxjs/operators";


import { AuthUser } from "./../models/auth-user";
import { Credentials } from "./../models/credentials";
import { ApiConfigService } from "./api-config.service";
import { APP_CONFIG, IAppConfig } from "./app.config";
import {tokenStoreService} from './tokenStoreService';
import {MenuRoutes} from '../../Models/MenuRoutes';


@Injectable({
  providedIn: 'root'
})
export class AuthService {


  constructor(
    private http: HttpClient,
    private router: Router,
    @Inject(APP_CONFIG) private appConfig: IAppConfig,
    private tokenStoreService:tokenStoreService,
    private apiConfigService: ApiConfigService
  ) {

  }

  authUser:AuthUser;
  public MenuChanged:EventEmitter<Array<MenuRoutes>> = new EventEmitter();

  login(credentials: Credentials): Observable<boolean> {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.http
      .post(`${this.appConfig.apiEndpoint}/${this.apiConfigService.configuration.LoginPath}`,
        credentials, { headers: headers })
      .pipe(
        map((response: any) => {
          if(response && response.Success) {
            if (response && response.Data.Access_token) {
              // store user details and jwt token in local storage to keep user logged in between page refreshes
              this.tokenStoreService.setloginInfo(response.Data.Access_token,credentials.RememberMe);
              this.authUser = response.Data;
              this.tokenStoreService.setCookie("XSRF-TOKEN",response.Data.XsrfToken,365);
              return true;
            }
          }
          else
          {
            alert(response.Message)
            return false;
          }
        }),
        catchError((error: HttpErrorResponse) => throwError(error))
      );
  }



  getUserinfo(){
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.http
      .post(`${this.appConfig.apiEndpoint}/auth/GetUserInfo`,
        { headers: headers })
      .pipe(
        map((response: any) => {
          if(response && response.Success) {
              this.authUser = (<AuthUser>response.Data);

              this.MenuChanged.emit((<AuthUser>response.Data).Menus);
              return this.authUser;
          }
          else
          {
            alert(response.Message)
            return null;
          }
        }),
        catchError((error: HttpErrorResponse) => throwError(error))
      );
  }



  logout(navigateToHome: boolean): void {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
     this.http
      .post(`${this.appConfig.apiEndpoint}/${this.apiConfigService.configuration.LogoutPath}`,
        { headers: headers })
      .pipe(
        map(response => response || {}),
        catchError((error: HttpErrorResponse) => throwError(error)))
      .subscribe((result:any) => {
        if(result.Success) {
          this.tokenStoreService.deleteAuthTokens();
          if (navigateToHome) {
            this.router.navigate(["auth/login"]);
          }
        }
        else
        {
          alert(result.Message);
        }
      });
  }

  isAuthUserLoggedIn(): boolean {
    if(this.authUser == null)
    {
      return false;
    }
    return true;
  }

  //check this to validate
  getAuthUser(): AuthUser | null {
    if (!this.isAuthUserLoggedIn()) {
      //this.logout(true);
      return null;
    }
    else
    {
      return this.authUser;
    }
  }

}
