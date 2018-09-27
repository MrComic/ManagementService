import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {NewUserModel} from '../Models/NewUserModel';
import {Observable} from 'rxjs';
import {HttpResult} from '../../../core/Models/http-result';

@Injectable()
export class UserServiceService {

  constructor(private http:HttpClient) {

  }

  NewUser(user:NewUserModel):Observable<HttpResult>
  {
    return this.http.post<HttpResult>('/api/user/NewUser',user,{ headers:new HttpHeaders({ 'Content-type':'application/json' }) });
  }
}
