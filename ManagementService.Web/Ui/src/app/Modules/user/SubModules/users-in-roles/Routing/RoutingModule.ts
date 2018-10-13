import { NgModule } from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {SharedModule} from '../../../../../shared/shared.module';

import {CommonModule} from '@angular/common';
import {UsersInRoleListComponent} from "../Components/users-in-role-list/users-in-role-list.component";
import {AddUserToRoleComponent} from "../Components/add-user-to-role/add-user-to-role.component";
import {ListComponent} from "../Components/list/list.component";


const routes:Routes  = [
  {path:'list/:userid',component:UsersInRoleListComponent}
]

@NgModule({
  declarations:[UsersInRoleListComponent,AddUserToRoleComponent,ListComponent],
  imports: [
    SharedModule,
    CommonModule,
    RouterModule.forChild(routes)
  ],
  providers:[],
  exports:[RouterModule]
})
export class UsersInRoleRoutingModule { }
