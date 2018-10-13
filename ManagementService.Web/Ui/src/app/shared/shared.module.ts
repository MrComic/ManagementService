import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {DataTablesModule} from 'angular-datatables';
import {ReactiveFormsModule} from "@angular/forms";
import { NgSelectModule } from '@ng-select/ng-select';
import {ngxLoadingAnimationTypes, NgxLoadingModule} from 'ngx-loading';

@NgModule({
  imports: [
    NgbModule,
    DataTablesModule.forRoot(),
    ReactiveFormsModule,
    NgSelectModule,
    NgxLoadingModule.forRoot({ animationType: ngxLoadingAnimationTypes.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.1)',
      backdropBorderRadius: '4px',
      primaryColour: '#ffffff',
      secondaryColour: '#ffffff',
      tertiaryColour: '#ffffff'}),
  ],
  exports:[NgbModule,DataTablesModule,ReactiveFormsModule,NgSelectModule,NgxLoadingModule],
  providers:[]
})
export class SharedModule { }
