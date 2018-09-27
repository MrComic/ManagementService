import { BrowserModule } from '@angular/platform-browser';
import {APP_INITIALIZER, NgModule} from '@angular/core';
import { AppComponent } from './app.component';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import {CoreModule} from './core/core.module';
import {SharedModule} from './shared/shared.module';
import {HeaderModule} from './header/header.module';
import { HomeComponent } from './MainModule/home/home.component';
import {RouterModuleModule} from './router-module/router-module.module';
import { NotFoundComponent } from './MainModule/not-found/not-found.component';
import { LoginComponent } from './MainModule/Auth/login/login.component';
import {ReactiveFormsModule} from '@angular/forms';
import { MainLayoutComponent } from './MainModule/main-layout/main-layout.component';
import { EmptyLayoutComponent } from './MainModule/empty-layout/empty-layout.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NotFoundComponent,
    LoginComponent,
    MainLayoutComponent,
    EmptyLayoutComponent

  ],
  imports: [
    RouterModuleModule,
    BrowserModule,
    HttpClientModule,
    CoreModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  providers: [
    // { provide: APP_INITIALIZER,
    //   useFactory: InitServiceProvider,
    //   deps: [InitService], multi: true }
      ],
  bootstrap: [AppComponent]
})
export class AppModule {



}
