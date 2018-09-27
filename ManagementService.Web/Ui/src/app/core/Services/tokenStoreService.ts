import {Injectable} from '@angular/core';
import {AuthUser} from '../Models/auth-user';

@Injectable({
  providedIn: 'root'
})
export class tokenStoreService {


    public setloginInfo(access_token:string,rememberme:boolean){
      if(rememberme)
      {
        window.localStorage.setItem('token',access_token);
      }
      else
      {
        window.sessionStorage.setItem('token',access_token);
      }
      this.setCookie('rememberme',rememberme,365);
    }

    public getToken()
    {
      if(this.getCookie('rememberme').toLowerCase() == 'true')
      {
        return window.localStorage.getItem('token');
      }
      else
      {
        return window.sessionStorage.getItem('token');
      }
    }


  hasToken(){
    return this.getToken() != '' &&  this.getToken() != null;
  }

  removeToken(){
    return this.deleteAuthTokens()
  }

    deleteCookie(name) {
      this.setCookie(name, "", 365);
    }

    deleteAuthTokens(){
      window.localStorage.removeItem('token');
      window.sessionStorage.removeItem('token');
      this.deleteCookie('rememberme');
    }


    setCookie(cname:string, cvalue:any, exdays:number) {
      var d = new Date();
      d.setTime(d.getTime() + (exdays*24*60*60*1000));
      var expires = "expires="+ d.toUTCString();
      document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    getCookie(cname:string) {
      var name = cname + "=";
      var decodedCookie = decodeURIComponent(document.cookie);
      var ca = decodedCookie.split(';');
      for(var i = 0; i <ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
          c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
          return c.substring(name.length, c.length);
        }
      }
      return "";
    }
}
