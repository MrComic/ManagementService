import {Inject, Injectable} from '@angular/core';
import { BehaviorSubject, Observable, throwError } from "rxjs";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import {AuthUser} from "../../Models/auth-user";
import { APP_CONFIG, IAppConfig } from "../app.config";
import {HttpResult} from "../../Models/http-result";
import {AuthService} from "../auth.service";
import { catchError, finalize, map } from "rxjs/operators";
@Injectable({
  providedIn: 'root'
})
export class EditprofileService {

  editprofileUrl=`${this.appConfig.apiEndpoint}/auth/editprofile`;
  GetuserIdUrl=`${this.appConfig.apiEndpoint}/auth/GetUserId`;
  constructor(private http:HttpClient, @Inject(APP_CONFIG) private appConfig: IAppConfig,private auths:AuthService) {

  }


  editprofile(editprofile:AuthUser ): Observable<HttpResult> {

    const headers = new HttpHeaders({ "Content-Type": "application/json",body:JSON.stringify(editprofile) });
    return this.http.post<HttpResult>(this.editprofileUrl,editprofile,{ headers: headers} ).pipe(
      map((response: HttpResult) => {
        if(response && response.Success) {
          if (response && response.Data.Access_token) {
            this.auths.authUser = response.Data
            return response.Data;

          }
        }
        else
        {
          alert(response.Message)
          return {};
        }
      }),
      catchError((error: HttpErrorResponse) => throwError(error))
    );
  }
  GetUserId(guid:string) {
    const headers = new HttpHeaders({ "Content-Type": "application/json" });
    return this.http.get<AuthUser>(this.GetuserIdUrl+"?userid="+guid,{ headers: headers } );

  }
}
