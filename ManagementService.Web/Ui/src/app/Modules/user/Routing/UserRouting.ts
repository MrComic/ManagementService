import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {userlistComponent} from "../userlist/userlist.component";
import {SharedModule} from '../../../shared/shared.module';
import {NewuserComponent} from "../newuser/NewUser.component";
import {UserServiceService} from '../Services/user-service.service';
import {OrgService} from '../Services/org.service';
import {CommonModule} from '@angular/common';


const routes:Routes  = [
  {path:'list',component:userlistComponent},
  {path:'newuser',component:NewuserComponent},
  {path:'usersinroles',  loadChildren:'../SubModules/users-in-roles/users-in-roles.module#UsersInRolesModule',}
]

@NgModule({
  declarations:[userlistComponent,NewuserComponent],
  imports: [
    SharedModule,
    CommonModule,
    RouterModule.forChild(routes)
  ],
  providers:[UserServiceService,OrgService],
  exports:[RouterModule]
})
export class UserRoutingModule { }
