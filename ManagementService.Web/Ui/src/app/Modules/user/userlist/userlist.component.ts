import {AfterViewChecked, AfterViewInit, Component, forwardRef, Host, Inject, OnInit, Renderer} from '@angular/core';
import {MainLayoutComponent} from "../../../MainModule/main-layout/main-layout.component";
import {Params, Router} from "@angular/router";
import {UserServiceService} from '../Services/user-service.service';
import {HttpResult} from '../../../core/Models/http-result';

@Component({
  selector: 'user-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class userlistComponent implements OnInit,AfterViewInit {

  dtOptions: DataTables.Settings = {};

  ngAfterViewInit(){

  }

  constructor(@Inject(forwardRef(() => MainLayoutComponent)) private parent:MainLayoutComponent
              ,private router:Router,private usermanager:UserServiceService) {
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

  UnlockUser(data:any){
    this.parent.showLoading();
    this.usermanager.UnlockUser(data.UserId).subscribe((data:HttpResult)=>{
      this.parent.hideLoading();
      if(data.Success)
      {
        alert('با موفقیت انجام شد')
      }
      else
      {
        alert(data.Message);
      }
    },(error)=>{
      this.parent.hideLoading();
      alert('مشکلی در انجام عملیات جاری وجود دارد');
    });
  }

  ManageRoles(data:any){
    this.router.navigate(['users','usersinroles', 'list',data.UserId ] );
  }

  ngOnInit(): void {



    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      serverSide: true,
      searchDelay: 2000,
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
        data: 'OrgName'
      },{
        data:'btn',
        searchable:false,
        orderable:false,
        title: 'مدیریت دسترسی',
        render: function (data: any, type: any, full: any) {
          console.log(data);
          return`
              <button class="btn btn-primary" data-command-RoleManagement="" >
                <i class="fa fa-shield-alt"></i>
                مدیریت سطح دسترسی
              </button>
              <button class="btn btn-success" data-command-Unlock="" >
                <i class="fa fa-unlock"></i>
                فعال سازی
              </button>
          `;
        }
      }],
      rowCallback: (row: Node, data: any[] | Object, index: number) => {
        const self = this;

        $('[data-command-RoleManagement]', row).off().on('click', () => {
          self.ManageRoles(data);
        });

        $('[data-command-Unlock]', row).off().on('click', () => {
          self.UnlockUser(data);
        });

        return row;
      }



    };
  }

}
