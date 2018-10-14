import { Component, OnInit } from '@angular/core';
import {InitUi} from "../../../../shared/SharedServices/InitJquery";

declare var $ :any;
@Component({
  selector: 'app-errorpage',
  templateUrl: './errorpage.component.html',
  styleUrls: ['./errorpage.component.css']
})
export class ErrorpageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    InitUi()



  }

}
