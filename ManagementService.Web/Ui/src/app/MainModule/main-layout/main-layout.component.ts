import {AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit} from '@angular/core';
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

  IsLoading:boolean = false;
  constructor(private cdRef : ChangeDetectorRef) {

  }

  showLoading()
  {
    this.IsLoading = true;
    $('input,button').attr('disabled','disabled');
    this.cdRef.detectChanges();
  }

  hideLoading()
  {
    this.IsLoading = false;
    $('input,button').removeAttr('disabled');
    this.cdRef.detectChanges();
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
