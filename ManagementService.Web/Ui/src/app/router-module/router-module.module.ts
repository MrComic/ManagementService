import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from '../MainModule/home/home.component';
import {NotFoundComponent} from '../MainModule/not-found/not-found.component';
import {LoginComponent} from '../MainModule/Auth/login/login.component';
import {EmptyLayoutComponent} from "../MainModule/empty-layout/empty-layout.component";
import {MainLayoutComponent} from "../MainModule/main-layout/main-layout.component";
import {AuthGuardService} from '../core/Services/auth-guard';
import {ErrorpageComponent} from "../core/components/ErrorPage/errorpage/errorpage.component";
import {AccessdeniedComponent} from "../core/components/AccessDenied/accessdenied/accessdenied.component";
import {ProfileComponent} from "../core/components/profile/profile.component";
import { EditprofileComponent } from '../core/components/profile/editprofile/editprofile.component';
const routes:Routes  = [

  {
    path:'users',
    component:MainLayoutComponent,
    loadChildren:'../Modules/user/user.module#UserModule',
    canActivate:[AuthGuardService]
  },
  {
    path:'auth',
    component:EmptyLayoutComponent,
    children:[
      {path:'login',component:LoginComponent}
    ]
  },

  {
    path:'error',
    component:EmptyLayoutComponent,
    children:[
      { path:'404',component:NotFoundComponent  },
      { path: '500', component: ErrorpageComponent  },
      { path: '403', component: AccessdeniedComponent  },
    ]
  },
  {
    path:'',
    component:MainLayoutComponent,
    children:[
      { path: '', component:HomeComponent }
    ],canActivate:[AuthGuardService]

  },
  {path:'profile',    component:MainLayoutComponent,children:[
    {path:'',component:ProfileComponent},
    {path:'editprofile/:userid',component:EditprofileComponent}
  ],canActivate:[AuthGuardService]},
  { path: '**', redirectTo: 'error/404'  }
  ]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class RouterModuleModule { }
