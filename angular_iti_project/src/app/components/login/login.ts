import { Component, NgModule, OnInit, Type } from '@angular/core';
import { Register } from '../service/register';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/Forms';
import { ILogin } from '../../models/Register';
import Swal from 'sweetalert2';
import { ProductService } from '../service/product-service';
import { Details } from '../details/details';

@Component({
  selector: 'app-login',
  imports: [CommonModule,FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {
  /**
   *
   */
  // token!:string;
  // cc!:typeof Details;
  // csc!:Type<Details>;
  error!:string;
  loginInfo!:ILogin;
  saveToLocalhost:boolean=false;
  isLogin!:boolean;
  registerSubscription!:Subscription;
  usernameOrEmail!:string;
  password!:string;
  constructor(private productService:ProductService,private _register:Register,private _router:Router){}
  ngOnInit(): void {
    this.registerSubscription=this._register.$isLogin.subscribe(bool=>this.isLogin=bool);
    // عاوز لما ارفرش الصفحة يشوف ال يو ار ال لو لقاه بيحتوي علي totalOrder 
    // يعمل نافيجيت ل لوجين لو هو مش isLogin &&isFindLocalHost ==false
  }

  login()
  {
// debugger;
    if(!this.usernameOrEmail||!this.password)
    {
        Swal.fire({
      icon: "error",
      title: "Oops...",
      text: "usernameOrEmail and password Is Required!!",
        });
    return;
    }
    //interface أو type	❌ مفيهوش new  تكتب الاقواس ع طول	{ ... }
    this.loginInfo={Password:this.password,UserOrEmail:this.usernameOrEmail}

    this.productService.login(this.loginInfo)
    .subscribe(
      value=>
      {
        // debugger;///////
        if(value.error)
            {
              this.error=value.error;
              return;
            }
              if(value.token)
              { 
                  if(this.saveToLocalhost)
                  {
                   localStorage.setItem('AngularToken',value.token);     
                  }else{
                    sessionStorage.setItem('AngularToken',value.token);     
                  }
            //  this._register.saveToLocalHost(value.token);
              }
              Swal.fire({
                title: "Login Successfully!",
                icon: "success",
                draggable: true
              });



      this._register.logIn(); /////

      if(this.isLogin)
      {
          this._router.navigate(["/totalOrder"]);
      }

      }
      

    );
  }
  logout()
  {
      this._register.logOut();
  }
  
}
