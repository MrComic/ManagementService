import {Role} from "./Role";

export class User{
  SetUser(userid,username,Roles,Orgid,accessToken)
  {
    this.UserId = userid;
    this.UserName = username;
  }

  UserId:string
  UserName:string;
  Roles:Array<Role>;
  Orgid:number;
}
