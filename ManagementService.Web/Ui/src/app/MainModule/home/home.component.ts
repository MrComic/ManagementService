import {Component, Host, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Credentials} from '../../core/Models/credentials';
import {AuthService} from "../../core/Services/auth.service";
import {MainLayoutComponent} from "../main-layout/main-layout.component";




@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService:AuthService,@Host() parent: MainLayoutComponent) {
    parent.TitleChanged({ Title : 'خانه' ,BreadCrumb:[{route :'' ,title:'خانه'}] });
  }

  ngOnInit() {
    //this.breadcrumbService.titleChanged({ Title : 'خانه' ,BreadCrumb:[{route :'' ,title:''}] });
  }

}
