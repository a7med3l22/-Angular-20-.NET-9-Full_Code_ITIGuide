import { Injectable } from '@angular/core';
import { Observable, Subscriber, UnsubscriptionError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Notifications {



  //عاوزه يبعت نوتفيكيشن ف الكونسول كل ثانيتين عن طريق ال اوبسيرفل 
  private notifications: (string | number)[];
  constructor() {
    this.notifications = [
      4,
      'Welcome',
      1,
      'To My',
      'Component',
      55,
      'Home',
      'This Is Ahmed Alaa',
      ' '
    ]
  }


//“الفانكشن اللي جوه الكلاس مباشرة تُسمى Method.”

  notificationsObservable(): Observable<string|number> {
    //(this: Observable<string>, subscriber: Subscriber<string>) => TeardownLogic
    // معناها انها بتاخد فانكشن وده شكل ال Arrowfunction
    // ممكن اعمله كده لو بياخد براميتر واحد     return new Observable<string>(subscriber => {})
    // انما لو مش بياخد براميترز او بياخد اكتر من براميتر لازم احط الاقواس كده return new Observable<string>(() => {}) أو return new Observable<string>((x,y,z) => {})
    // انما هو بياخد فانكشن بتاخد براميتر واحد ف ممكن احط اقواس او لا 
    // وممكن ابعت الفانكشن دي علي هيئة arrow function او فنكشن عادية 
    // لو انا استخدمت arrow function واستخدمت this جواها هتبقي ال this دي من نوع الكلاس عادي 
    // انما لو استخدمت Function Expression  ف ال this اللي جواها هتبقي عبارة عن this: Observable<string> 
    // وهو ده معني ال this (this: Observable<string>, subscriber: Subscriber<string>)
    // مش معناها انها بتاخد براميتر اسمه وليكن this من نوع Observable<string> خالص 
    //return new Observable<string>(subscriber => {this.notifications}) // وده شكل ال arrow function 
    //return new Observable<string>(function(subscribe){this.subscribe()}) // وده شكل ال Function Expression
    //return new Observable<string>((subscriber):void => {this.notifications}) // ممكن اعملها كده بحيث اقول انها بترجع فويد
    //return new Observable<string>(function(subscribe):void{this.subscribe()}) //ممكن اعملها كده بحيث اقول انها بترجع فويد
    //TeardownLogic بتاخد
    // ودي لما يحصل unsubscribe أو complete أو error. بيتم تنفيذها 
    // وبتبقي حاجة م ال 3 
    //1- return; لو مش عاوز حاجة تحصل يبقي ارجع فويد
    //2- return ()=>{} أو retrun function(){}  لو عاوز اعمل لوجيك جوه ال فانكشن يبقي ارجع فانكشن
    //3- return Subscription  => يعني لو عملت سبسكريب داخليا زي كده const sub = interval(1000).subscribe(val => subscriber.next(`Value: ${val}`));
    // ف انتا اعمل ريتيرن ليها كده   return sub; 
    // علشان يعملها unsubscribe كده sub.unsubscribe();
    // لما يحصل unsubscribe لل Observable اللي هي فيه 
    //<3
    //لو عاوز احدد النوع اللي هترجعه ال ارو فانكشن لازم احطها بين قوسين حتي لو بتاخد براميتر واحد بس 
    //filter((value):boolean =>value instanceof NavigationEnd) زي كده 
    return new Observable<(string|number)>(subscriber => {
      // subscriber.next();
      // subscriber.error();
      // subscriber.complete();
      var count = 0;
      //setInterval=> setInterval(handler: TimerHandler, timeout?: number, ...arguments: any[]): number; => TimerHandler = string | Function;
      // debugger;


      const repeatCode = (loveMessage: string): void => {
        if (count == this.notifications.length) {
          subscriber.next("");
          subscriber.complete(); // هيروح لل ريتيرن // [غلق الـ Observable.]
          // هيروح ينادي ع الفانكشن دي return () => {} 
          // وبعد م ينفذها هيرجع هنا تاني ف انا عاوزة لما يرجع مينفذش اي حاجة تاني ويخرج ف اعمل return;
           return; // ريتيرن معناها ب الظبط اكني بقوله ارجع فورًا للي نده للميثود اللي انا فيها دي ومتنفذش أي حاجة تانية جوه الميثود دي.  
          //RxJS يتجاهل تمامًا أي مكالمات next, error, أو complete بعد الإغلاق.
        }

        if (typeof this.notifications[count]==="string"&&!this.notifications[count].toString().trim()) {
          subscriber.next("");
          subscriber.error("Message Is Empty!!"); // [غلق الـ Observable.]
           return; // ريتيرن معناها ب الظبط اكني بقوله ارجع فورًا للي نده للميثود اللي انا فيها دي ومتنفذش أي حاجة تانية جوه الميثود دي.  
        }
        subscriber.next(this.notifications[count]); //بعد ثانيتين هيطبع دي 
        //عاوزة يستني ثانيتين كمان ويبعت دي 
        console.log(loveMessage);
        count++;
      }
      //TimerHandler = string | Function;
      const interval = setInterval(repeatCode, 2000, "<3"); // الباريميتر الثالث ده هيتبعت للبراميتر بتاع الفانكشن
      //repeatCode("<3")  اكنها بتعمل كده كل ثانيتين 
      //عاوز اعمل كلير لل سيت انتيرفال لما يحصله كومبليت او ايرور 
      /*
          RxJS يتوقع منك إرجاع function ليقوم بتنفيذها تلقائيًا عند:
            هذه الدالة تسمى Teardown Logic.
            تُسمى هذه Teardown Logic، وهي تُنفذ تلقائيًا في 3 حالات:

1️⃣ عند استدعاء unsubscribe() يدويًا من المشترك.
2️⃣ عند حدوث complete() داخل الـ Observable.
3️⃣ عند حدوث error() داخل الـ Observable.
      */

      return () => {
        // debugger;
        clearInterval(interval);
      };

    }


    )
  }
}
/* 
       var x=repeatCode("<3")   // دي هتعمل كول للفانكشن وهترجعلك ال ريتيرن بتاعها ف ال اكس هنا هتبقي فويد لانها هترجع فويد
       var y=repeatCode   // ال واي نفسها خي ال فانكشن يعني عشان استدعيها وانفذها اعمل كده y("<3")
*/
/*
ub.complete();
    sub.next('Ahmed'); // يتم تجاهلها بالكامل
هنا 
    sub.next('Ahmed'); // يتم تجاهلها بالكامل
ده هيتنفذ بس مش هيبعت حاجة لل سبسكرايبر صح ؟
✅ بالضبط، كلامك صحيح 100%.
*/
/*

            اشكال الفانكشنز   
                             (c:string):void=>{this.notifications};
        ////
        const repeatCodes=   (c:string):void=>{this.notifications};
        ////
                            function repeatCodess(c:string):void{this.}; // error مينفعش استخدم This هنا
        ////
        const repeatCodesss=function repeatCodesssss(c:string):void{this.};// error مينفعش استخدم This هنا
        ////
        function repeat(){};    
        const repeatt = function() {};     
     
   ///////////////داخل الكلاس ///////////////
             repeatCode = () => {
                  // your logic here
              }
              repeatcode()
              {
                
              }
        الخلاصة الذهبية:
        🔹 داخل الكلاس: تعريف الميثود بدون كلمة function.
        🔹 داخل الميثود: استعمل Function Expression أو Arrow Function.


        // بتستخدم يساوي فقط لو عاوز تساوي قيمة بقيمة !!
*/