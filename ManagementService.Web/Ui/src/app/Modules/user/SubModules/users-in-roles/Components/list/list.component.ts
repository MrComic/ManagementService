import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UsersInRoleModel} from "../../Models/UsersInRoleModel";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {

  @Input('UsersInRoles') UsersInRoles:UsersInRoleModel[] = [];

  @Output('DeleteRequest') DeleteRequest:EventEmitter<{roleid:string,userid:string}> = new EventEmitter<{roleid:string,userid:string}>()

  constructor() { }

  ngOnInit() {
  }

  DeleteUserFromRole(roleid:string,userid:string)
  {
    this.DeleteRequest.emit({roleid,userid});
  }

}
