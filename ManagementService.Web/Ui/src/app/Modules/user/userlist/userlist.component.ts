import {AfterViewChecked, AfterViewInit, Component, Host, OnInit, Renderer} from '@angular/core';
import {MainLayoutComponent} from "../../../MainModule/main-layout/main-layout.component";
import {Router} from "@angular/router";

@Component({
  selector: 'user-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class userlistComponent implements OnInit,AfterViewInit {

  //dtTrigger: Subject = new Subject();
  dtOptions: DataTables.Settings = {};

  ngAfterViewInit(){

  }

  constructor(@Host() parent: MainLayoutComponent,private router:Router) {
      parent.TitleChanged({ Title:'مدیریت کاربران' ,
        BreadCrumb:[
          { route : "" , title: "خانه" },
          { route : "/users/list" , title: "مدیریت کاربران" }
        ]
      });
  }

  AddUser()
  {
    this.router.navigate(['users/newuser'])
  }



  ManageRoles(data:any){
       console.log(data.UserId )
  }

  ngOnInit(): void {



    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 2,
      serverSide: true,
      processing: true,
      language: {
        "url": "/assets/DataTablesPersian.json"
      },
      ajax: 'api/user/list',
      columns: [{
        title: 'شناسه کاربری',
        data: 'UserId',visible:false
      }, {
        title: 'نام',
        data: 'FullName'
      }, {
        title: 'نام کاربری',
        data: 'UserName'
        }, {
          title: 'ایمیل',
          data: 'Email'
        }, {
        title: 'نام سازمان',
        data: 'OrgId'
      },{
        data:'btn',
        searchable:false,
        orderable:false,
        title: 'مدیریت دسترسی',
        render: function (data: any, type: any, full: any) {
          console.log(data);
          return`
              <button class="btn btn-default" data-command-RoleManagement="" >مدیریت سطح دسترسی</button>
          `;
        }
      }],
      rowCallback: (row: Node, data: any[] | Object, index: number) => {
        const self = this;

        $('[data-command-RoleManagement]', row).off().on('click', () => {
          self.ManageRoles(data);
        });
        return row;
      }



    };
  }

}
