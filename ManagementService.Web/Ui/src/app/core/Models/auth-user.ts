import {MenuRoutes} from '../../Models/MenuRoutes';

export interface AuthUser {
  UserId: string;
  UserName: string;
  Firstname: string;
  LastName:string;
  ImageLink:string;
  Email:string;
  Roles: string[] | null;
  Menus: Array<MenuRoutes> | null;
  Access_token:string;
  ExpireDate:Date;
  PhoneNumber:string;
  NationalCode:string;
  MobileNumber:string;


}
