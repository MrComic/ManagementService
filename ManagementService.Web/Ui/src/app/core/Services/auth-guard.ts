import {EventEmitter, Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanLoad, Data, Route, Router, RouterStateSnapshot} from '@angular/router';
import {AuthService} from './auth.service';
import {observable, Observable, throwError} from 'rxjs';
import {map} from 'rxjs/internal/operators';
import {catchError} from 'rxjs/operators';
import {HttpErrorResponse} from '@angular/common/http';
import {AuthUser} from "../Models/auth-user";
import {MenuRoutes} from '../../Models/MenuRoutes';
import {tokenStoreService} from "./tokenStoreService";
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate
  //, CanActivateChild, CanLoad
 {

  private permissionObjectKey = "permission";



  constructor(private authService: AuthService, private router: Router, private tokenStoreService:tokenStoreService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    //const permissionData = route.data[this.permissionObjectKey] as AuthGuardPermission;
    const returnUrl = state.url;
    try {
      return this.hasAuthUserAccessToThisRoute(null, returnUrl)
    }
    catch {
      this.showErrorPage();
      return new Observable((observer) => {

        // observable execution
        this.showLoginPage(returnUrl);
        observer.next(false);
        observer.complete()
      })
    }
  }

  // canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
  //   //const permissionData = childRoute.data[this.permissionObjectKey] as AuthGuardPermission;
  //   const returnUrl = state.url;
  //   return this.hasAuthUserAccessToThisRoute(null, returnUrl);
  // }
  //
  // canLoad(route: Route): boolean {
  //   if (route.data) {
  //     //const permissionData = route.data[this.permissionObjectKey] as AuthGuardPermission;
  //     const returnUrl = `/${route.path}`;
  //     return this.hasAuthUserAccessToThisRoute(null, returnUrl);
  //   } else {
  //     return true;
  //   }
  // }

  private hasAuthUserAccessToThisRoute(permissionData: Data, returnUrl: string): Observable<boolean> {

    if (!this.authService.isAuthUserLoggedIn()) {
      if(this.tokenStoreService.hasToken() ) {

      return this.authService.getUserinfo()
         .pipe(map((response:AuthUser)=>{

           if(response == null)
           {
             alert('خطا در دریافت اطلاعات کاربری');
             this.showLoginPage(returnUrl);
             return false;
           }
           else
           {
                return true;
           }
         }),catchError((error: HttpErrorResponse) => {
                   if(error.status == 401)
                   {
                     this.showLoginPage(returnUrl);
                   }
                   if(error.status == 500)
                   {
                     this.showErrorPage();
                   }
                   return throwError(error);
         }));
      }
      else {
         return new Observable((observer) => {

          // observable execution
          this.showLoginPage(returnUrl);
          observer.next(false);
          observer.complete()
        })
      }
    }
    else {
      return new Observable((observer) => {
        observer.next(true);
      })
    }
  }

  private showLoginPage(returnUrl: string) {
    this.router.navigate(["auth/login"], { queryParams: { returnUrl: returnUrl } });
  }


   private showErrorPage() {
     this.router.navigate(["error/500"]);
   }

  private showAccessDenied(returnUrl: string) {
    this.router.navigate(["error/403"], { queryParams: { returnUrl: returnUrl } });
  }
}
