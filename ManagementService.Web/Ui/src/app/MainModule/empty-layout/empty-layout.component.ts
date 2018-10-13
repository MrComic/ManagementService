import {AfterViewInit, ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {InitJquery} from "../../SharedServices/InitJquery";

@Component({
  selector: 'app-empty-layout',
  templateUrl: './empty-layout.component.html',
  styleUrls: ['./empty-layout.component.css']
})
export class EmptyLayoutComponent implements OnInit,AfterViewInit {
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

  ngOnInit() {
  }

  ngAfterViewInit(){
    InitJquery();
  }
}
