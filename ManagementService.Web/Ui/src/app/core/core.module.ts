import {APP_INITIALIZER, NgModule} from '@angular/core';
import { CommonModule } from '@angular/common';
import {HeaderModule} from '../MainModule/header/header.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import {RouterModule} from "@angular/router";
import {RouterModuleModule} from "../router-module/router-module.module";
import {APP_CONFIG, AppConfig} from './Services/app.config';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {XsrfInterceptor} from './Services/xsrf.interceptor';
import {ApiConfigService} from './Services/api-config.service';
import {JwtInterceptor} from './Services/JwtInterceptor';
import { ErrorpageComponent } from './components/ErrorPage/errorpage/errorpage.component';
import { AccessdeniedComponent } from './components/AccessDenied/accessdenied/accessdenied.component';
import { ProfileComponent } from './components/profile/profile.component';
import {MenuItemComponent} from "./components/menu-item/menu-item.component";
import {SharedModule} from "../shared/shared.module";
import { EditprofileComponent } from './components/profile/editprofile/editprofile.component';
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  imports: [
    RouterModuleModule,
    CommonModule,HeaderModule,  ReactiveFormsModule
  ],
  providers:[
    {
      provide: APP_CONFIG,
      useValue: AppConfig
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: XsrfInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AuthInterceptor,
    //   multi: true
    // },
    {
      provide: APP_INITIALIZER,
      useFactory: (config: ApiConfigService) => () => config.loadApiConfig(),
      deps: [ApiConfigService],
      multi: true
    }
  ],
  declarations: [SidebarComponent, ErrorpageComponent, AccessdeniedComponent, ProfileComponent,MenuItemComponent, EditprofileComponent],
  exports:[SidebarComponent,HeaderModule,ProfileComponent,EditprofileComponent]
})
export class CoreModule { }
