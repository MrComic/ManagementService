import {EventEmitter, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {InitDataModel} from "../Models/InitDataModel";
import {AuthService} from './OAuthServices/AuthService';
import {User} from 'oidc-client';

@Injectable()
export class InitService {

  public data:InitDataModel;

  public UserAuthenticated:EventEmitter<User>;


  constructor(private httpservice:HttpClient,private authService:AuthService) {}

  loadDefaultData(){
    if(this.authService.isLoggedIn()) {
      console.log(this.authService.getAuthorizationHeaderValue())
    }
    else
      {
      console.log("Unauthorized")
      }

    // UserLogin
return null
    // if(this.authService.isLoggedIn()) {
    //
    //   const promise = new Promise<InitDataModel>((resolve, reject) => {
    //     var p = this.httpservice.get<InitDataModel>('/api/BaseData')
    //
    //     p.subscribe((data) => {
    //         console.log(data);
    //         this.data = data;
    //         resolve(data);
    //       },
    //       (error) => {
    //         reject("error loading Application");
    //         console.log("error loading Application")
    //       });
    //   })
    //   return promise;
    // }
  }

  getPreloadedData():InitDataModel{
    return this.data;
  }
}
