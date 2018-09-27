import {AfterViewInit, Component, Host, OnInit} from '@angular/core';
import {AuthService} from "../../Services/auth.service";
import {AuthUser} from "../../Models/auth-user";
import {MainLayoutComponent} from "../../../MainModule/main-layout/main-layout.component";
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit,AfterViewInit {
  user:AuthUser;

  constructor(private profile:AuthService,@Host() parent: MainLayoutComponent) {
      this.user=this.profile.getAuthUser();
      parent.TitleChanged({ Title:'خانه' , BreadCrumb:[{ route : "" , title: "خانه" },{ route : "/profile" , title: "پروفایل" }] });
  }
  ngAfterViewInit(){

  }
  ngOnInit() {

  }

}
