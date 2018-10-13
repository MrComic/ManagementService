import {Component, forwardRef, Host, Inject, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {UsersInRoleService} from "../../Services/users-in-role.service";
import {Observable, of, Subject} from "rxjs/index";
import {UsersInRoleModel} from "../../Models/UsersInRoleModel";
import {catchError} from "rxjs/internal/operators";
import {RoleModel} from "../../Models/RoleModel";
import {HttpResult} from "../../../../../../core/Models/http-result";
import * as _ from 'lodash'
import {MainLayoutComponent} from "../../../../../../MainModule/main-layout/main-layout.component";
@Component({
  selector: 'app-users-in-role-list',
  templateUrl: './users-in-role-list.component.html',
  styleUrls: ['./users-in-role-list.component.css']
})
export class UsersInRoleListComponent implements OnInit {


  UserId:string = this.route.snapshot.params['userid'];

  userRoles:UsersInRoleModel[] = [];


  constructor(private route:ActivatedRoute,private userRoleService:UsersInRoleService,
              @Inject(forwardRef(() => MainLayoutComponent)) private parent:MainLayoutComponent) {
    parent.TitleChanged({ Title:'مدیریت کاربران' ,
      BreadCrumb:[
        { route : "" , title: "خانه" },
        { route : "/users/list" , title: "مدیریت کاربران" },
        { route : "/users/usersinroles/list/"+this.UserId , title: "مدیریت نقش کاربران" }
      ]
    });
  }



  UserAddedToRole(event:UsersInRoleModel){
    this.userRoles.push(event);
  }

  DeleteUserFromRole(event:{roleid:string,userid:string})
  {
    this.parent.showLoading();
    this.userRoleService.RemoveUserFromRole(event.userid,event.roleid).subscribe((data)=>{
      this.parent.hideLoading();
      if(data.Success) {
         _.remove(this.userRoles, (arr) => {
           return arr.UserId == event.userid && arr.RoleId == event.roleid
         });
       }
       else
       {
         alert(data.Message);
       }
    },(error)=>{
      this.parent.hideLoading();
      alert('مشکلی در انجام عملیات جاری وجود دارد')
    });
  }


  ngOnInit() {

      this.parent.showLoading();

      this.userRoleService.GetUserRoles(this.UserId).subscribe((data:UsersInRoleModel[])=>{
        //this.loadingService.hideLoading();
        this.userRoles  = data;
        this.parent.hideLoading();
      }, ((error) => {
        this.parent.hideLoading();
        alert('مشکلی در بازیابی اطلاعات وجود دارد');
        return of<UsersInRoleModel[]>();
      }));


  }

}
