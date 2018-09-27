import {Component, Host, OnInit} from '@angular/core';
import {MainLayoutComponent} from "../../../MainModule/main-layout/main-layout.component";
import {Router} from "@angular/router";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {DEFAULT_DROPDOWN_CONFIG} from "../../../DefaultSelectizeConfig";
import {OrgService} from '../Services/org.service';
import {OrgModel} from '../Models/OrgModel';
import {Observable} from 'rxjs';
import {UserServiceService} from '../Services/user-service.service';
import {NewUserModel} from '../Models/NewUserModel';
import {HttpErrorResponse} from '@angular/common/http';

@Component({
  selector: 'app-newuser',
  templateUrl: './NewUser.component.html',
  styleUrls: ['./NewUser.component.css']
})
export class NewuserComponent implements OnInit {

  orgs: Observable<OrgModel[]>;

  constructor(@Host() parent: MainLayoutComponent,
              private router:Router,
              private orgServie:OrgService,
              private userService:UserServiceService
  ) {

    parent.TitleChanged({ Title:'مدیریت کاربران' ,
      BreadCrumb:[
        { route : "" , title: "خانه" },
        { route : "/users/list" , title: "مدیریت کاربران" },
        { route : "/users/newuser" , title: "افزودن کاربر" },
      ]
    });
  }


   OrgSelectConfig: any = Object.assign({}, DEFAULT_DROPDOWN_CONFIG, {
    labelField: 'Name',
    valueField: 'OrgId',
    plugins: ['remove_button'],
    maxItems: 1
   });

  form:FormGroup=new FormGroup({
    'Firstname':new FormControl(null,[Validators.required]),
    'LastName':new FormControl(null,[Validators.required]),
    'NationalCode':new FormControl(null,[Validators.required,Validators.minLength(10)]),
    'PhoneNumber':new FormControl(null,[Validators.required]),
    'Password':new FormControl(null,[Validators.required,Validators.minLength(8)]),
    'Rpassword':new FormControl(null,[Validators.required,this.SameValidator.bind(this)]),
    'Email':new FormControl(null,[Validators.required]),
    //'UserName':new FormControl(null,[Validators.required]),
    'OrgId':new FormControl(null  ,[Validators.required])
  });

  reset()
  {
    this.form.reset();
  }

  ngOnInit() {
    this.orgs = this.orgServie.GetOrgs();
  }

  SubmitForm()
  {
    if(this.form.valid)
    {
      this.userService.NewUser((<NewUserModel>this.form.value)).subscribe(data=>{
            if(data.Success)
            {
              this.router.navigate(['users','list']);
            }
            else
            {
              alert(data.Message);
            }
      },(error:HttpErrorResponse)=>{
          alert('مشکلی در انجام عملیات وجود دارد');
      });
    }
  }

  SameValidator(formControl:FormControl):{[s:string] :boolean}{
    var password = this.form;
    if(password)
    {
      console.log(password.get('Password'))
      if(password.get('Password').value == formControl.value) {
        return null;
      }
      else
      {
        return {'SamePassword':true}
      }
    }
    return null;
  }


}
