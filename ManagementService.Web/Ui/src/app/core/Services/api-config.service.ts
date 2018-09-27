import {Inject, Injectable, Injector} from '@angular/core';
import {APP_CONFIG, IAppConfig} from './app.config';
import {HttpClient} from '@angular/common/http';
import {tokenStoreService} from './tokenStoreService';
import {AuthService} from './auth.service';
declare var $ :any;
@Injectable({
  providedIn: 'root'
})
export class ApiConfigService {

  private config: IApiConfig | null = null;

  constructor(
    private injector: Injector,
    @Inject(APP_CONFIG) private appConfig: IAppConfig) { }

  loadApiConfig(): Promise<any> {
  //  $(".preloader").show();
    const http = this.injector.get<HttpClient>(HttpClient);
    const xsrfTokenStoreService = this.injector.get<tokenStoreService>(tokenStoreService);
    const url = `${this.appConfig.apiEndpoint}/${this.appConfig.apiSettingsPath}`;
   // const authService = this.injector.get<AuthService>(AuthService)
    var jwtToken ='';
    if(xsrfTokenStoreService.hasToken())
    {
      jwtToken = xsrfTokenStoreService.getToken();
    }
    return http.get<InitApi>(url + '?t='+jwtToken)
      .toPromise()
      .then(config => {
        //$(".preloader").fadeOut();
        this.config = config.Config;
        if(config.Xsrftoken != '' && config.AccessToken)
        {
          xsrfTokenStoreService.setCookie("XSRF-TOKEN",config.Xsrftoken,365);
        }
        if(config.AccessToken != '' && config.AccessToken)
        {
          var re = xsrfTokenStoreService.getCookie('rememberme');
          xsrfTokenStoreService.setloginInfo(config.AccessToken, re ==null || re== "" ? true : re.toLowerCase() == 'true' ? true : false)
        }
        else
        {
          if(xsrfTokenStoreService.hasToken())
          {
            xsrfTokenStoreService.removeToken();
          }
        }

      })
      .catch(err => {
        //$(".preloader").fadeOut();
        console.error(`Failed to loadApiConfig(). Make sure ${url} is accessible.`, this.config);
        return Promise.reject(err);
      });
  }

  get configuration(): IApiConfig {
    if (!this.config) {
      throw new Error("Attempted to access configuration property before configuration data was loaded.");
    }
    return this.config;
  }
}

export interface InitApi{
  Config:IApiConfig;
  Xsrftoken:string;
  AccessToken:string;
}

export interface IApiConfig {
  LoginPath: string;
  LogoutPath: string;
  RefreshTokenPath: string;
  AccessTokenObjectKey: string;
  RefreshTokenObjectKey: string;
  AdminRoleName: string;
}
