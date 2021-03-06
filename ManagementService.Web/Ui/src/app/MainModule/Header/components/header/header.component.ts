import { Component, OnInit } from '@angular/core';
import {Messages} from "../../../../Models/Messages";
import {AuthUser} from "../../../../core/Models/auth-user";
import {AuthService} from "../../../../core/Services/auth.service";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  _appMessages:Array<Messages>;

  authuser:AuthUser;

  constructor(private authservice:AuthService) {
    this.authuser = authservice.getAuthUser();
  }

  ngOnInit() {

  }

  SignOut(){
    this.authservice.logout(true);
  }

  RedirectToMessage(message: Messages) {

  }
}
