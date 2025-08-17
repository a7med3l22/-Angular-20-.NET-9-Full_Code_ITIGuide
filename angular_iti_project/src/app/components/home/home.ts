import { Component, OnDestroy, OnInit } from '@angular/core';
import { Notifications } from '../service/notifications';
import { filter, map, Subscription } from 'rxjs';
import { Register } from '../service/register';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnDestroy, OnInit {
  /**
   *
   */
  Islogin!:boolean;
  // IshaveToken!:boolean;
  notification!: number;
  //subscription:Subscription = new Subscription(); // ممكن كده او ك اختصار اللي تحتها 
  subscription = new Subscription();
  constructor(private _notifications: Notifications,private _register:Register) {
  }
  ngOnInit(): void {
 // عاوز تفهم اي حاجة ادخل جوه السورس كود اللي عاملاه الانجولر وافهم لما تعمل ادد مثلا اي بيحصل ولما تعمل ان سبسكرايب اي بيحصل وهتفهم كل حاجة :>
  this.subscription.add(this._register.$isLogin.subscribe(value=>this.Islogin=value));
  // this.subscription.add(this._register.$isHaveLocalHost.subscribe(value=>this.IshaveToken=value));

      // this._register.$isLogin.subscribe(value=>this.Islogin=value);



    // this.registerSubscribe=this._register.$isLogin.subscribe(bool=>this.Islogin=bool)


    return;
    /*
    //(method) Observable<string>.subscribe(observerOrNext?: Partial<Observer<string>> | ((value: string) => void) | undefined): Subscription
    // يعني بترجع Subscription  : Subscription
    //Partial<Observer<string>> | ((value: string) => void) | undefined
    // يعني جواها بتستخدم حاجة من 3 
    // 1- اوبجيكت من نوع Partial<Observer<string>> يعني بتاخد اوبجيكت من نوع Observer<string> وفيه كل البروبيرتي اختياريه ده معني بارشل
    // 2- تستخدم فانكشن بتاخد براميتر ومش بترجع حاجة
    // 3- ممكن مترجعش اي حاجة
    */

    // عاوز اي استرنج جاي بيحتوي علي ahmed معرضهوش
    this.subscription = this._notifications.notificationsObservable().pipe(
      filter(
      // ادلوقتي مع تحديث التايب اسكريبت ممكن اعمله زي تحت كده ويبفهم عادي جدا انما قبل كده مكانش بيفهم ف الافضل اني اعمله كده  //(value):value is number => typeof(value)==="number"   // :value is number  عملت كده علشان يرجعلي مصفوفة من النامبر مباشرة مش من استرنج اور نامبر 
     value => typeof(value)==="number"   // لو ف مشروع قديم هتلاقيها مكتوبة كده (value):value is number => typeof(value)==="number"  لان التايب اسكريبت وقتها مكانش بيعرف النوع اللي راجع غير لما اعمله كده 
      ) ,
       map(value=>value==55?22:value) 
    )
.subscribe(  //Partial<Observer<string>> يعني بتاخد اوبجيكت من نوع Observer<string> وفيه كل البروبيرتي اختياريه ده معني بارشل
      {
        next: (value) => {
           this.notification = value ;
           console.log(value)
          },
        complete: () => { console.log('complete!') },
        error: (message) => { console.log(message) },
      })
  }


  ngOnDestroy(): void { // بتتنادي لما الكمبوننت ده يتشال !
  
    this.subscription.unsubscribe();
  // this.registerSubscribe.unsubscribe();
  
    return;
    this.subscription.unsubscribe();
  }
  // logIn()
  // {
  //   // debugger;
  //   this._register.logIn();
  // }
  // logOut()
  // {
  //      this._register.logOut();
  // }

}
