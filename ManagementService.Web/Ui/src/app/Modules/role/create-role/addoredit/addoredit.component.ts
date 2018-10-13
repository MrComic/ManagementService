import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';


import {RoleViewModel} from "../../Models/RoleViewModel";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {RoleService} from "../../Services/RoleService";
import {Router} from "@angular/router";


@Component({
  selector: 'app-addoredit',
  templateUrl: './addoredit.component.html',
  styleUrls: ['./addoredit.component.css']
})
export class AddoreditComponent implements OnInit {
  @ViewChild('Name') tempname:ElementRef;

@Input() Role:RoleViewModel;

@Output()  Refresh_datatables= new EventEmitter();


  constructor(private RoleServices:RoleService,private  router:Router) {

  }

  ngOnInit():void {



  }

  AddRole(name:string){

    if(name.trim().length>0)
    {
      this.RoleServices.CreateRole(name).subscribe(data=>{

        if(data.Success)
        {
          //this.router.navigate(['']);
          this.Refresh_datatables.emit();
          this.tempname.nativeElement.value="";
          alert(data.Message);
        }
        else
        {
          alert(data.Message);
        }
      });
    }
    else {
      alert("لطفا نام نقش را وارد نمایید.");
    }

  }
  EditRole(name:string,roleid:string)
  {
    if(name.trim().length>0)
    {
      this.RoleServices.EditRole(name,roleid).subscribe(data=>{

        if(data.Success)
        {

          this.Refresh_datatables.emit();
          this.tempname.nativeElement.value="";
          alert(data.Message);
        }
        else
        {
          alert(data.Message);
        }
      });
    }
    else {
      alert("لطفا نام نقش را وارد نمایید.");
    }
  }

}
