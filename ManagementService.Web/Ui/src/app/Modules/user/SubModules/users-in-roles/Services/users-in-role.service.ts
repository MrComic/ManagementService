import { Injectable } from '@angular/core';
import {Observable} from "rxjs/index";
import {UsersInRoleModel} from "../Models/UsersInRoleModel";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {RoleModel} from "../Models/RoleModel";
import {HttpResult} from "../../../../../core/Models/http-result";

@Injectable()
export class UsersInRoleService {

  constructor(private http:HttpClient) { }

  GetUserRoles(userid:string):Observable<UsersInRoleModel[]>{
        return this.http.get<UsersInRoleModel[]>('api/user/ListUserRoles',{ params:{ 'userid':userid } });
  }

  AddToRole(roleid:string,userid:string):Observable<HttpResult>{
    return this.http.post<HttpResult>('api/user/AddToRole'
      ,null, {params:{ userid:userid , rolename:roleid } ,headers:new HttpHeaders({ 'Content-type':'application/json' })});
  }

  RemoveUserFromRole(userid:string,roleid:string):Observable<HttpResult>
  {
    return this.http.post<HttpResult>('api/user/RemoveUserFromRole',null,
      {params:{  userid:userid,roleid:roleid },headers:new HttpHeaders({ 'Content-type':'application/json' })});
  }

  GetRoles():Observable<RoleModel[]>{
    return this.http.get<RoleModel[]>('api/user/Roles');
  }
}
