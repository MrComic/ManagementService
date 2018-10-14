import { BrowserModule } from '@angular/platform-browser';
import { NgModule} from '@angular/core';
import { AppComponent } from './app.component';
import { HttpClientModule} from '@angular/common/http';
import {CoreModule} from './core/core.module';
import {SharedModule} from './shared/shared.module';
import { HomeComponent } from './MainModule/home/home.component';
import {RouterModuleModule} from './router-module/router-module.module';
import { NotFoundComponent } from './MainModule/not-found/not-found.component';
import { LoginComponent } from './MainModule/Auth/login/login.component';
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
