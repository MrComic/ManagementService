import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {DataTablesModule} from 'angular-datatables';
import {ReactiveFormsModule} from "@angular/forms";
import { NgSelectModule } from '@ng-select/ng-select';
@NgModule({
  imports: [
    NgbModule,
    DataTablesModule.forRoot(),
    ReactiveFormsModule,
    NgSelectModule
  ],
  exports:[NgbModule,DataTablesModule,ReactiveFormsModule,NgSelectModule]
})
export class SharedModule { }
