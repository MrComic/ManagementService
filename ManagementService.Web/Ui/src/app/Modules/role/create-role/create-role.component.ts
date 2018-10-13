import {AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {RoleViewModel} from "../Models/RoleViewModel";
import {DataTableDirective} from "angular-datatables";
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.css'],

})
export class CreateRoleComponent implements OnInit {
  @ViewChild(DataTableDirective)
  dtElement:DataTableDirective;
  dtOptions:DataTables.Settings={};
  Role:RoleViewModel={Name:"",RoleId:""};


  dtTrigger: Subject<any> = new Subject();

  constructor() {}



  ngOnInit() :void{
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      searchDelay:2000,
      serverSide: true,
      processing: true,
      responsive:true,
      language: {
        "url": "/assets/DataTablesPersian.json"
      },
      ajax: 'api/user/RoleList',
      columns: [{
        title: 'شناسه نقش',
        data: 'RoleId',
        // visible:false
      }, {
        title: 'نام نقش',
        data: 'Name'
      },{
        data:'btn',
        searchable:false,
        orderable:false,
        title: 'ویرایش نقش',
        render: function (data: any, type: any, full: any) {

          return`
              <button class="btn btn-default" data-command-EditRole="" >
                <i class="fa fa-arrow-circle-up"></i>
                ویرایش 
              </button>
             
          `;

        }
      }],
      rowCallback: (row: Node, data:RoleViewModel , index: number) => {
        const self = this;
        $('[data-command-EditRole]', row).off().on('click', () => {
          if(data==null)
          {
           alert('مشکل درانجام عملیات')
          }
          else
          {

           self.Role=data;

          }
        });

        return row;
      }



    };

  }
  ngAfterViewInit(): void {
    this.dtTrigger.next();
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();

      this.dtTrigger.next();

    });
  }


}
