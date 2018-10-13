import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {UserRoutingModule} from "./Routing/UserRouting";
import {ReactiveFormsModule} from "@angular/forms";
import { ListComponent } from '../../Modules/user/SubModules/users-in-roles/Components/list/list.component';

@NgModule({
  imports: [
    CommonModule,
    UserRoutingModule,ReactiveFormsModule
  ],
  declarations: []
})
export class UserModule {

}
