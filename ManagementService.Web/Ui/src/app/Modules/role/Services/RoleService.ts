import { Injectable } from '@angular/core';
import {Observable} from "rxjs/index";

import {HttpClient, HttpHeaders} from "@angular/common/http";

import {HttpResult} from "../../../core/Models/http-result";


@Injectable()
export class RoleService {

  constructor(private  http:HttpClient)
  {

  }
  EditRole(Name:string,RoleId:string)
  {

    return this.http.post<HttpResult>('/api/auth/EditRole',JSON.stringify({Name,RoleId}),{headers:new HttpHeaders({'content-type':'application/json'})});
  }
  CreateRole(Name:string):Observable<HttpResult>{

    return this.http.post<HttpResult>('/api/auth/CreateRole',JSON.stringify(Name),{headers:new HttpHeaders({'content-type':'application/json'})});
  }
}
