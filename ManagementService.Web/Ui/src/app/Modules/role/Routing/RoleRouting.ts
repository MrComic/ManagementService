import {RouterModule, Routes} from "@angular/router";
import {CreateRoleComponent} from "../create-role/create-role.component";
import {AddoreditComponent} from "../create-role/addoredit/addoredit.component";
import {CommonModule} from "@angular/common";
import {SharedModule} from "../../../shared/shared.module";
import {NgModule} from "@angular/core";
import { FormsModule } from '@angular/forms';
import {RoleService} from "../Services/RoleService";

const routes:Routes=[
  {path:'createrole',component:CreateRoleComponent},
  {path:'',component:AddoreditComponent}
  ]

@NgModule({
  declarations:[CreateRoleComponent,AddoreditComponent],
  imports: [
    SharedModule,
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  providers:[RoleService],
  exports:[RouterModule]
})
export class RoleRoutingModule { }
