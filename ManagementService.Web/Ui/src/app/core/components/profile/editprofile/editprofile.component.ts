import {Component, Host, OnInit} from '@angular/core';
import {EditprofileService} from '../../../Services/user/editprofile.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {MainLayoutComponent} from "../../../../MainModule/main-layout/main-layout.component";
import {AuthUser} from "../../../Models/auth-user";
@Component({
  selector: 'app-editprofile',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofileComponent implements OnInit {

  error:string="";
  userid:string="";

  form:FormGroup=new FormGroup({
    'UserId':new FormControl(null),
    'Firstname':new FormControl(null,[Validators.required]),
    'LastName':new FormControl(null,[Validators.required]),
    'NationalCode':new FormControl(null,[Validators.required]),
    'MobileNumber':new FormControl(null,[Validators.required]),
    'PhoneNumber':new FormControl(null,[Validators.required]),
    'Email':new FormControl(null,[Validators.required])

  });

  constructor(private editprofileservice:EditprofileService,private  router:Router,@Host() parent: MainLayoutComponent,private r:ActivatedRoute) {
     parent.TitleChanged({ Title:'خانه' , BreadCrumb:[{ route : "" , title: "خانه" },{ route : "/profile" , title: "پروفایل" },{ route : "/profile/editprofile" , title: "ویرایش پروفایل" }]} );
  }

  ngOnInit() {
    console.log();
    this.editprofileservice.GetUserId(this.r.snapshot.params.userid).subscribe(getuser=>{
      this.form.setValue(getuser);
    })
  }


  onSubmit() {

      if(this.form.valid)
      {
        var tempuser=(<AuthUser>this.form.value);

        tempuser.UserId = this.r.snapshot.params.userid;

        this.error = "";
        this.editprofileservice.editprofile(tempuser)
          .subscribe(editprofile=>{
          if(editprofile.Success)
          {

            this.router.navigate(['profile']);
          }
          else {

            alert(editprofile.Message);
          }


        },(error: HttpErrorResponse) => {
          console.error("Login error", error);
          if (error.status === 500) {
           alert("مشکل در انجام عملیات")
          } else {
            this.error = `${error.statusText}: ${error.message}`;
          }
          });
      }
  }


}
