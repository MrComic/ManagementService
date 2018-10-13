import {Component, Host, OnInit} from '@angular/core';
import {EditprofileService} from '../../../Services/user/editprofile.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {MainLayoutComponent} from "../../../../MainModule/main-layout/main-layout.component";
import {AuthUser} from "../../../Models/auth-user";
import {HttpResult} from "../../../Models/http-result";
@Component({
  selector: 'app-editprofile',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})
export class EditprofileComponent implements OnInit {

  error:string="";
  userid:string="";

  form:FormGroup=new FormGroup({
    'UserId':new FormControl(""),
    'Firstname':new FormControl("",[Validators.required]),
    'LastName':new FormControl("",[Validators.required]),
    'NationalCode':new FormControl("",[Validators.required,Validators.minLength(10),Validators.maxLength(10)]),
    'MobileNumber':new FormControl("",[Validators.required,Validators.minLength(11),Validators.maxLength(11)]),
    'PhoneNumber':new FormControl("",[Validators.required]),
    'Email':new FormControl("",[Validators.required])

  });

  constructor(private editprofileservice:EditprofileService,private  router:Router,@Host() parent: MainLayoutComponent,private r:ActivatedRoute) {
     parent.TitleChanged({ Title:'خانه' , BreadCrumb:[{ route : "" , title: "خانه" },{ route : "/profile" , title: "پروفایل" },{ route : "/profile/editprofile" , title: "ویرایش پروفایل" }]} );
  }

  ngOnInit() {

    this.editprofileservice.GetUserId(this.r.snapshot.params.userid).subscribe(getuser=>{
      this.form.setValue(getuser);
    })
  }


  onSubmit() {

      if(this.form.valid)
      {

        var tempuser=(<AuthUser>this.form.value);



        this.editprofileservice.editprofile(tempuser)
          .subscribe(editprofile=>{



          if(editprofile.Success)
          {
            alert(editprofile.Message);
            this.router.navigate(['profile']);
          }
          else {

            alert(editprofile.Message);
          }


        },(error: HttpErrorResponse) => {

          if (error.status === 500) {
           alert("مشکل در انجام عملیات")
          } else {
            this.error = `${error.statusText}: ${error.message}`;
            console.log(this.error);
          }
          });
      }
  }


}
