import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Register  {
//بتنشأ أول مرة يتم طلبها ، وبتفضل موجودة طول عمر التطبيق. 
  private _isLogin=new BehaviorSubject<boolean>(false);
  $isLogin=this._isLogin.asObservable();

  private _isHaveLocalHost=new BehaviorSubject<boolean>(localStorage.getItem('AngularToken')?true:false); // دي القيمة الابتدائية جامد والله
  $isHaveLocalHost=this._isHaveLocalHost.asObservable();    //////
  //  savedToLocalHost:boolean=false;

  // private _savedToLocalHost=new BehaviorSubject<boolean>(false);
  // $savedToLocalHost=this._savedToLocalHost.asObservable();

  // عاوز لما اضغط ع لوجن يروح يكلم دي من الهوم وبعدين يحط توكن ف اللوكل استوردج ويخلي istoken variable ب ترو

  logIn()
  {
    // localStorage.setItem('AngularToken',"hefsffsfsfljualfhakfhaflafg");
    this._isLogin.next(true);
  }
  logOut()
  {

    // if(this._isHaveLocalHost)
    // {
    //   localStorage.removeItem('AngularToken');
    //   // this._isHaveLocalHost.next(false);

    // }


        localStorage.removeItem('AngularToken');//لإن removeItem() لو المفتاح مش موجود، مش بيعمل حاجة ومش بيطلع Error، فمش محتاج تفحص قبله.

       sessionStorage.removeItem('AngularToken');

        this._isLogin.next(false);
  }
  // saveToLocalHost(token:string)
  // {
  //   localStorage.setItem('AngularToken',token);     
  //   // عاوزة كل م يحفظ حاجة ف ال توكين يخلهالي ترو ولما امسح حاجة يخلهالي ب فولس والهوم يعرف وعاوزة يعمل سبسكرايب ع ال يو ار ال واول م يتغير يعمل اتشيك اذا كان موجود لوكال هوست ولا لا ولو موجوده يخليها ترو ولو مش موجوده يخليها ب فولس 
  //   // اكتشفت ان لما اضغط ع تسجيل خروج هينفذ ال لوج اوت وبعدين هيعمل سبسكرايب ع الناف بار لان ال يو ار ال اتغير ووقتها هيكتشف ان مفيش لوكال هوست ف يكفي اني اعمل اتشيك لما ال يو ار ال يتغير فقط 
  //   // نفذ يله 
  //   // 
  //   // this.savedToLocalHost=true
  //   // this._isHaveLocalHost.next(true);
  // }
  isFindLocalHost():void
  {
    if(localStorage.getItem('AngularToken'))
    {
       this._isHaveLocalHost.next(true);
    }else{
       this._isHaveLocalHost.next(false);
    }

  }
}
//عاوزة كل م يدخل علي الهوم يشوف اذا كان بيحتوي علي لوكال هوست ولا لا 