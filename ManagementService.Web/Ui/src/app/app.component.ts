import {AfterViewInit, Component, OnInit, ViewEncapsulation} from '@angular/core';

import {InitJquery} from './SharedServices/InitJquery'
import {
  ActivatedRoute, NavigationEnd, Route, Router, RouterState, RouterStateSnapshot,
  UrlSegment
} from '@angular/router';
import { filter} from "rxjs/internal/operators";
import {AuthService} from './core/Services/auth.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  encapsulation:ViewEncapsulation.None
})
export class AppComponent implements OnInit,AfterViewInit{


  constructor(private authService:AuthService,
              private route:ActivatedRoute,private router: Router){
    InitJquery();
  }

  ngAfterViewInit(){

  }

  ngOnInit(): void {

  }

}
