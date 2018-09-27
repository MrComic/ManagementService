import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../SharedServices/OAuthServices/AuthService';
import {InitService} from '../../SharedServices/init.service';
import {User} from '../../Models/User';
import {User as identityUser} from 'oidc-client';
@Component({
  selector: 'app-authcallback',
  templateUrl: './authcallback.component.html',
  styleUrls: ['./authcallback.component.css']
})
export class AuthcallbackComponent implements OnInit {

  constructor(private authService: AuthService,private initService:InitService) { }

  ngOnInit() {
    this.authService.completeAuthentication().then(user=> {
      //this.initService.data.User = new User();//.SetUser(user.) ;
      console.log(user);
      this.initService.UserAuthenticated.emit(this.authService.getuser())
    });;
  }

}
