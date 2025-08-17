import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormArray, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators} from '@angular/Forms';
import { IRegister } from '../../models/Register';
import { IProduct } from '../../models/iproduct';
import { ProductService } from '../service/product-service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule,CommonModule,FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register implements OnInit {
  /**
   *
   */
  constructor(private productService:ProductService,private router:Router) {
  
    
  }
  register!:IRegister;
  token!:string;
  errors!:string[];
/*
  {
    "displayName": "string",
    "address": {
      "firstName": "string",
      "lastName": "string",
      "street": "string",
      "city": "string",
      "country": "string"
    },
    "email": "user@example.com",
    "password": "string",
    "confirmPassword": "string",
    "phoneNumber": "string"
  }
*/
get displayFirstName()
{
  return this.registerForm.get('displayName')?.get('displayFirstName');
}
get displaylastName()
{
  return this.registerForm.get('displayName')?.get('displayLastName');
}
get addressFirstName()
{
  return this.registerForm.get('address')?.get('firstName');
}
get addressLastName()
{
  return this.registerForm.get('address')?.get('lastName');
}
get street()
{
  return this.registerForm.get('address')?.get('street');
}
get city()
{
  return this.registerForm.get('address')?.get('city');
}
get country()
{
  return this.registerForm.get('address')?.get('country');
}
get email()
{
  return this.registerForm.get('email');
}
get password()
{
  return this.registerForm.get('password');
}
get confirmPassword()
{
  return this.registerForm.get('confirmPassword');
}
get phoneArray()
{
  
  //    get<P extends string | readonly (string | number)[]>(path: P): AbstractControl<ɵGetProperty<TRawValue, P>> | null;
  return (this.registerForm.get('phoneNumber') as FormArray)
}
registerForm=new FormGroup({ // موجو مباشر ف ال كلاس
  
displayName:new FormGroup({
  displayFirstName:new FormControl('',[this.required]),
  displayLastName:new FormControl('',[Validators.required]),
}),
address:new FormGroup({
firstName:new FormControl('',[Validators.required]),
lastName:new FormControl('',[Validators.required]),
street:new FormControl('',[Validators.required]),
city:new FormControl('',[Validators.required]),
country:new FormControl('',[Validators.required]),
}),
/*
interface ValidatorFn {
    (control: AbstractControl): ValidationErrors | null; // الانترفيس ده 
} 
*/

email:new FormControl('',[
  // بتاخد فانكشن علي الشكل ده  (control: AbstractControl): ValidationErrors | null; 
  // زي اللي عملتها تحت كده لكن الافضل طبعا استخدم الحاجات الجاهزة معدش اختراع العجلة يعني 
  this.required,Validators.email]), //required(control: AbstractControl): ValidationErrors | null;// email(control: AbstractControl): ValidationErrors | null;
password:new FormControl('',[Validators.required]),
confirmPassword:new FormControl('',[Validators.required,this.confirmPass]),
phoneNumber:new FormArray([
  new FormControl('',[Validators.required,this.validatePhone])
]) // max 3 
},[this.confirmPass]);


required(control: AbstractControl): ValidationErrors | null   //type ValidationErrors = { [key: string]: any; };
{
 return control.value?null:{'Required Ya Ahmed':true};
}
confirmPass(control: AbstractControl): ValidationErrors | null
{
  
  let pass=control.get('password')?.value;
  let confirmPass=control.get('confirmPassword')?.value;
  if (!pass || !confirmPass) return null; // مفيش مقارنة لو فاضيين مهمه دي علشان يرجع نال لو مفيش فاليو اصلا 
  return (pass==confirmPass)?null:{'PassNotMatched':true};
}
push()
{
  if(this.phoneArray.length<3)
  {  
   this.phoneArray.push(new FormControl('',[Validators.required,this.validatePhone]));
  }
}
validatePhone(control: AbstractControl): ValidationErrors | null {
  const value = control.value;
  if (!value) return null; 
  if (isNaN(value)) return { 'MustBeNumber': true };
  if (value.length != 11) return { 'notPhoneNumber': true }; // هييجي هنا لو فيه فاليو ومش نامبر 
  return null;
}

remove(idx:number)
{
  if(this.phoneArray.length>1)
  {
    // * To change the controls in the array, use the `push`, `insert`, `removeAt` or `clear` methods
    this.phoneArray.removeAt(idx);
  }
}

ngOnInit(): void {
  
}



submitForm()
{
  // debugger;
  if (this.registerForm.invalid) {
    this.registerForm.markAllAsTouched(); // لما اضغط سبمت اكني عملت تاتش لكل حاجة ف الفورم 
    return;
  }
  this.register=this.registerForm.value as IRegister; // [برافو عليك ده اوبجيكت هو زي ال IRegister ب الظبط ف بعرفه وبقوله ان انتا تشوفه ك انه IRegister ف عاش]
  this.productService.register(this.register)
  .subscribe(

    value=>{
      if(!value.errors)
      {
        Swal.fire({
          title: "Registered Successfully!",
          html: `
          Your UserName Is:<b>${value.userName}</b>
          <br>
          Your Email Is:<b>${value.email}</b>`,
          icon: "success",
          draggable: true
        });
          this.router.navigate(['/login']);
       
      }
      else{
        this.errors=value.errors;
      }

    }
  )
  
  ;
}
}
/*

    أعملها ف الباك داتا بيز جديده خاصة ب ال لوجين بتاع ال ITI 
    وظبط الجزء ده كويس 
        {
          "displayName": {
            "displayfirstName": "Ahmed",
            "displaylastName": "Alaa"
          },
          "address": {
            "firstName": "Ahmed",
            "lastName": "Alaa",
            "street": "Behind The Faculty Of Commerce",
            "city": "20",
            "country": "مصر"
          },
          "email": "ahmedaladdinmohamed@gmail.com",
          "password": "2222",
          "confirmPassword": "2222",
          "phoneNumber": [
            "01558561998",
            "01279428817"
          ]
        }
 */