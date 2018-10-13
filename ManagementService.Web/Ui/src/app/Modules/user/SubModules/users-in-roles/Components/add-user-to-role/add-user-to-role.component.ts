import {Component, EventEmitter, forwardRef, Host, Inject, Input, OnInit, Output} from '@angular/core';
import {Observable, of} from "rxjs/index";
import {RoleModel} from "../../Models/RoleModel";
import {catchError} from "rxjs/internal/operators";
import {UsersInRoleService} from "../../Services/users-in-role.service";
import {ActivatedRoute} from "@angular/router";
import {HttpResult} from "../../../../../../core/Models/http-result";
import {UsersInRoleModel} from "../../Models/UsersInRoleModel";
import {MainLayoutComponent} from "../../../../../../MainModule/main-layout/main-layout.component";
import {hostElement} from "@angular/core/src/render3/instructions";

@Component({
  selector: 'app-add-user-to-role',
  templateUrl: './add-user-to-role.component.html',
  styleUrls: ['./add-user-to-role.component.css']
})
export class AddUserToRoleComponent implements OnInit {


  constructor(private route:ActivatedRoute,private userRoleService:UsersInRoleService,
              @Inject(forwardRef(() => MainLayoutComponent)) private _parent:MainLayoutComponent) {
    console.log(route,userRoleService);
    console.log(this._parent);
  }

    @Output('UserAddedToRole')UserAddedToRole:EventEmitter<UsersInRoleModel>=new EventEmitter<UsersInRoleModel>();

    @Input("UserId") UserId:string = ""

    Roles:Observable<RoleModel[]> = this.userRoleService.GetRoles();


  addUserToRole(roleid){
    this._parent.showLoading();
    this.userRoleService.AddToRole(roleid[0].Name,this.UserId).subscribe((data:HttpResult)=>{
      if(data.Success) {
        this._parent.hideLoading();
        this.UserAddedToRole.emit({ UserId :  this.UserId ,RoleName: roleid[0].Name , RoleId : roleid[0].Id});
        alert('با موفقیت انجام شد');

      }
      else
      {
        alert(data.Message);
      }
    },(error)=>{
      this._parent.hideLoading();
      alert('مشکلی در انجام عملیات جاری وجود دارد');
    })
  }


  ngOnInit() {

  }

}
