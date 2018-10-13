import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {UsersInRoleRoutingModule} from "./Routing/RoutingModule";
import {UsersInRoleService} from "./Services/users-in-role.service";

@NgModule({
  imports: [
    CommonModule,UsersInRoleRoutingModule
  ],
  declarations: [],
  providers:[UsersInRoleService]
})
export class UsersInRolesModule { }
