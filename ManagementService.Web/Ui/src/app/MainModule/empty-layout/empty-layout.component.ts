import {AfterViewInit, Component, OnInit} from '@angular/core';
import {InitJquery} from "../../SharedServices/InitJquery";

@Component({
  selector: 'app-empty-layout',
  templateUrl: './empty-layout.component.html',
  styleUrls: ['./empty-layout.component.css']
})
export class EmptyLayoutComponent implements OnInit,AfterViewInit {

  constructor() { }

  ngOnInit() {
  }

  ngAfterViewInit(){
    InitJquery();
  }
}
