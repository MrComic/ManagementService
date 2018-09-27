import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpXsrfTokenExtractor } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import {tokenStoreService} from "./tokenStoreService";

@Injectable()
export class XsrfInterceptor implements HttpInterceptor { // Handles absolute URLs
  constructor(private tokenExtractor: HttpXsrfTokenExtractor ,private TokenstoreService:tokenStoreService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.method === "POST") {
      const headerName = "X-XSRF-TOKEN";
      const token = this.tokenExtractor.getToken();
      if (token && !request.headers.has(headerName)) {
        request = request.clone({
          headers: request.headers.set(headerName, token)
        });
      }
    }
    return next.handle(request);
  }
}
