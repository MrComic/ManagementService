import { Component, OnInit } from '@angular/core';
import {InitDataModel} from "../../../Models/InitDataModel";
import {MenuRoutes} from "../../../Models/MenuRoutes";
import {AuthService} from '../../Services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  menu :Array<MenuRoutes>;


  constructor(public authservice:AuthService) {

  }

  ngOnInit() {
    this.menu = this.authservice.authUser.Menus;
    this.authservice.MenuChanged.subscribe((menu)=>{
      this.menu = menu;
    });
  }

}
