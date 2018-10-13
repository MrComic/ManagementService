import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RoleRoutingModule} from "./Routing/RoleRouting";
import {ReactiveFormsModule} from "@angular/forms";



@NgModule({
  imports: [
    CommonModule,
    RoleRoutingModule,ReactiveFormsModule
  ],
  declarations: []
})
export class RoleModule {

}
