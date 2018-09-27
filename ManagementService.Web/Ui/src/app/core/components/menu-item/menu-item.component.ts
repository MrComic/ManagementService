import {AfterViewInit, Component, Input, OnInit} from '@angular/core';
import {MenuRoutes} from "../../../Models/MenuRoutes";
import {initMenu} from "../../../SharedServices/InitMenu";

@Component({
  selector: 'app-menu-item',
  templateUrl: './menu-item.component.html',
  styleUrls: ['./menu-item.component.css']
})
export class MenuItemComponent implements OnInit,AfterViewInit {

  ngAfterViewInit(): void {
    initMenu();
  }

  @Input('menu') menu:Array<MenuRoutes>;

  constructor() { }

  ngOnInit() {

  }

  HasChild(id:number){
    return this.menu.filter(p=>p.ParentId == id).length>0 ;
  }

  getParent(){
    return this.menu.filter(l=>l.ParentId == -1);
  }

  Getchilds(id:number):Array<MenuRoutes>
  {

       let selectedchildren = this.menu.filter(p => p.ParentId == id);
       if (selectedchildren.length > 0) {
         return selectedchildren;
       }

  }


}
