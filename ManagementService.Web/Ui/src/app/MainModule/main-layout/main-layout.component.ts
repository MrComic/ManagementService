import {AfterViewInit, ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {InitJquery} from "../../SharedServices/InitJquery";
import {BreadCrumbModel} from '../../core/Models/BreadCrumbModel';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css'],
  // changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainLayoutComponent implements OnInit,AfterViewInit{
  bModel:BreadCrumbModel =new BreadCrumbModel();

  constructor() {

  }

  TitleChanged( bModel:BreadCrumbModel ){
    this.bModel = bModel;
  }

  ngAfterViewInit(){
    InitJquery();
  }

  ngOnInit() {
    // this.bModel = this.breadcrumbService.bmodel;
    // this.breadcrumbService.TitleChanged.subscribe((data)=>{
    //     this.bModel = data;
    // });
  }

}
