import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import {tokenStoreService} from './tokenStoreService';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private tokenStoreService:tokenStoreService)
  {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
   // let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    var access_token = this.tokenStoreService.getToken();
    if (access_token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${access_token}`
        }
      });
    }

    return next.handle(request);
  }
}
