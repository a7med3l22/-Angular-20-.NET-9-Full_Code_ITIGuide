import { Routes } from '@angular/router';
import { NotFound } from './components/not-found/not-found';
import { AboutUs } from './components/about-us/about-us';
import { Product } from './components/product/product';
import { Home } from './components/home/home';
// import * as order  from './components/order/order';
import { Details } from './components/details/details';
import { TotalOrderprice } from './components/total-order-price/total-order-price';
import { Login } from './components/login/login';
import { authGuardGuard } from './Guards/auth-guard-guard';
import { serverGuard } from './Guards/server-guard';
import { BackendError } from './components/backend-error/backend-error';
import { AddProdcut } from './components/add-prodcut/add-prodcut';
import { EditProduct } from './components/edit-product/edit-product';
import { Type } from '@angular/core';
import { Order } from './components/order/order';
import { Register } from './components/register/register';
/*
✅ إذًا كلامك فعلاً مضبوط:
"الكمبوننتس اللي مش بتنتمي للراوتر أوتليت بتتنفذ الأول، وبعدين الجاردات بتتفذ قبل ما يدخل الكمبوننت اللي جوه الراوتر أوتليت"

✅ صح 100%.

*/

export const routes: Routes = [
{path:'home',component:Home},
{path:'Edit/:id',canActivate:[serverGuard],component:EditProduct},
{path:'register',component:Register},
{path:'addProduct',component:AddProdcut,canActivate:[serverGuard]},
{path:'totalOrder',component:TotalOrderprice,canActivate:[serverGuard],children:[
{path:'',redirectTo:'order',pathMatch:'full'},
{path:'order',component:Order
  // loadComponent:()=>import('./components/order/order').then(val=>
    
  //   {
  //     // val: typeof import("e:/angular_iti_mona_assuit/angular_iti_mona_assuit/angular_iti_project/src/app/components/order/order")=
  //     // val=order;  // متاخدش ف بالك انا كنت بدي مثال بس اني ممكن اخد القيمة اللي جيالك واعيد تعريفها عادي بحاجة من نفس النوع بتاعها 
  //     return val.Order;
  //   }) // ادخل جوه ال then وافهم
//Gold    // انا بشرح بصيغة ال c#
          // اقدر انادي ع ال then دي من اي حاجة من نوع بروميس لانها جوه فانكشن نوعها بروميس  
          // ف اللي بيحصل ان ال then دي بتستقبل براميتر ضمنيا انا مش ببعته اللي هو الفاليو من البروميس اللي جواه
          // وبتستقبل فانكشن انا ببعتها 
          // وداخليا بقي جوه ال then ميثود بيعمل انفوك لل فانكشن اللي بعتهاله 
          // وبيبعتلها ف البراميتر ال فاليو اللي هو اخدها من ال بروميس اللي هو فيه 
          // بعد كده لما يحصل انفوك لل فانكشن اللي اتبعتتله وبعتلها البراميتر ف بيدخل جوه ال فانكشن دي بينفذ اللي فيها وبعدين يرجع تاني 
          // والفانكشن دي رجعت val.Order ف هو داخل ال then ميثود بيستقبلها بقي ويقوم مرجعلي بروميس من الفاليو اللي هو استقبلها من الفانكشن اللي بعتهاله  
}
,
{path:'details/:id',canActivate:[serverGuard],
  loadComponent:()=>import('./components/details/details').then(val=>val.Details)//وهترجع برومس من ال الريتيرن بتاع الفانكشن اللي اخدها دي   
  // الكمبوننت هتتحمل ف الراوتر لما ادخل جواها
  // loadComponent: x // أو

},
]},
{path:'ErrorFromBackEnd',component:BackendError},
{path:'login',component:Login},
// {path:'logout',component:Home},

{path:'aboutUs',component:AboutUs},
{path:'notFound',component:NotFound},
{path:'',redirectTo:'totalOrder',pathMatch:'full'}, //هذا المسار سيتم تفعيله عندما يكون الرابط فارغ بعد الدومين.
{path:'**',component:NotFound}
];
// loadComponent: x 
function x()
{
// let y:typeof Details;
// let tx:Type<Details>;
  // let y:Promise<Type<Details>>;
  // let y:Promise<typeof Details>; 
  // y=import('./components/details/details').then(val=>val.Details);
  // return y;
  // return import('./components/details/details').then(val=>val.Details);
//Gold    // ال امبورت دي عبارة عن بروميس من الموديول
          // ف لما يدخل الكمبوننت البروميس بتتنفذ ويبقي عبارة عن بروميس من الموديول
          // والبروميس دي فيه عليه ميثود اسمها then 
          // الميثود دي ببعتلها فانكشن وهو داخليا بيبعتلها القيمة بتاعت الفاليو 
          // ف اللي بيحصل ان لما بيعمل انفوك للفانكشن دي بترجعله قيمة 
          // ف القيمة دي بيستقبلها جوه ال then وال then ميثود ترجعلي promise من الفاليو دي 
          // ولو عملت then تاني ع ال then الاولي 
          //  هترجعلي بروميس من الفاليو اللي رجعتها من الفانكشن اللي ف ال then التانيه 
          // المهم ان ال فانكشن x دي بترجع بروميس من الكمبوننت لما ادخل الكمبوننت
          // ف ال loadComponent: بتستقبل ال بروميس دي وتخزن ف الراوتس الفاليو بتاعتها اللي هي الكمبوننت يعني 
          // بس كده 
          // اقول عليه ميثود مش فيه ميثود 
          //الفرق بين "فيه" و "عليه"
          //فيه → حاجة متخزنة جواه كـ data (property أو field)
          //عليه → حاجة متاحة عليه كـ behavior (method) بس مش محفوظة كـ data
    
}
/*
   الحمد لله انا كده فهمت تمام وسيبك بقي م اللي تحت التعليق ده 

     constructor(private _register:Register) {}
     ngOnInit() { this._register.$isHaveLocalHost.subscribe(value=>)}
 
     (اولا لما اعمل ريفريش للصفحة او ادخل ع لنك م الصفحة) 
     اول حاجة لما بيدخل للكمبوننت ده بيبتدي ينفذ الكونستراكتور بتاعه و ب التالي بينشئ اوبجيكت من ال سيرفس ريجيستر ولو السرفس ريجيستر اول مرة يتم استدعائها ف بيتم انشاء اوبجيكت ال سيرفس ريجيستر ب القيم الابتدائية لل بروبيرتس اللي فيه
     ولما يدخل علي ال  ngOnInit بيبتدي يعرف ان الريجيستر معموله سبسكرايب   ف يقوم واخد ال  فاليو ويبعتهالك ك فاليو جوه السبسكرايب 
     
     // وبعد م الكمبوتتنس يتنشء خلاص لو حصل اي تغيير ف ال قيمة اللي انا عامل سبسكرايب عليها يتنبعتلي القيمة الجديده دي وبيدخل جوه السبسكرايب 

        وب المثل 
         this.router.events.subscribe(value=>);
        1- لما اعمل ريفريش للصفحة او ادخل ع لنك م الصفحة بيتم انشاء ايفينتس من الراوتر والايفينتس دي بيفضل موجوده لو الكمبوننتس مش بتنتمي لل اوت ليت لان اول م يدخل ف الكمبوننت الي بتنتمي ل راوتر اوت ليت مش بيبقي موجود  ايفينتس من الراوتر

        ف لما يدخل علي ngOnInit() {   this.router.events.subscribe(value=>)} ف الكمبونانتس اللي مش بتنتمي للراوتر اوت ليت بيلاقي ايفينتس ف بالتالي بيدخل جوه السبسكرايب 

        2- بعد م الصفحة تحمل لو حصل تغيير ف الراوتر لنك  بيبتدي يدخل جوه السبسكرايب للكمبوننتس الي كانت حيه قبل تغيير اللنك وبتفضل حيه بعد تغيير اللنك لاننه دخل عليها مرة وعرف انها عامله سبسكرايب لل ايفينت ف بيبعتلها القيمة 
        
  ملحوظة اضافيه :-
           لما عدي ع دي 
      this.router.events.pipe(
          filter((value):value is NavigationEnd =>value instanceof NavigationEnd)
          ).subscribe(
            value=> );
      ف اول لفة عرف اني عملت سبسكرايب علي ال راوتر ايفينتس بس مدخلش جواها لان مكانش ال ايفينت موجود ف لما لاليفينت بقي موجود دخل ف سبسكرايب لانه خلاص عرف اني عملت سبسكرايب ع الايفينت من اول لفه 
عاش<3   
    //
    




*/



/*
مهم جداا 

this.router.events

1- لما اعمل ريفريش للصفحة او ادخل ع لنك م الصفحة 


يتم إنشاء الكمبوننتات:

أولًا: الكمبوننتات خارج <router-outlet> (مثل Navbar).

وبعد م يتم انشاءها بيبتدي يتعمل نيكيست ل  
this.router.events 
و ب التالي اي كمبوننت عامل سبسكرايب ليه م اللي تم انشاءها بياخد القيمة اللي اتعملها نيكست ف this.router.events 

وبعد كده يتم انشاء باقي الكمبوننانتس ال بتنتمي لل راوتر اوت ليت 
------
2- 
لما الراوتر لينك يتغير بيتعمل نيكست ل this.router.events 
و ب التالي الكمبونانتس الحيه وقت م الراوتر لنك بيتغير  وبتفضل موجوده وحيه بعد م الراوتر لينك بيتغير
يعني مش بيحصلها ديستوري اللي بتعمل سبسكرايب ليه بيتبعتلها القيمة دي فورا 
*/
/*
مهم جدا جدا جدا 


            يحصل بابليشر اول م الراوتر لينك بيتغير و
الكمبوننت اللي بتبقي حيه وقت م الراوتر لنك بيتغير  وبتفضل موجوده وحيه بعد م الراوتر لينك بيتغير
يعني مش بيحصلها ديستوري  لو بتعمل سبسكرايب علي البابلشر لينك ف بيدخل جوه السبسكرايب 
 لو هي بتعمل سبسكرايب للراوتر لنك طبعا 
,
ولما اعمل ريفريش للصفحة او ادخل ع لنك م الصفحة ف الانجولر
🔹 ما الذي يحصل عند أول تحميل (Refresh):

يتم تحميل Angular بالكامل.

يتم إنشاء الكمبوننتات:

أولًا: الكمبوننتات خارج <router-outlet> (مثل Navbar).

ثم: يبدأ الـ Router في عمل Navigation للمسار الحالي.

عند وصول NavigationEnd, يتم إنشاء الكمبوننت داخل <router-outlet>.
علشان كده 
لو فيه كمبوننت داخل <router-outlet> بتعمل Subscribe على this.router.events
مش بيوصلها ال نافيجيشن اند لانها بتتنفذ بعد م النافيجيشن اند بتخلص ف بالتالي مش هتدخل جوه السبسكرايب اللي هي عملاه 

// عانيت علي م وصلت للكومنت ده 
*/


/*
this.router.events

1- لما اعمل ريفريش للصفحة او ادخل ع لنك م الصفحة 


يتم إنشاء الكمبوننتات:

أولًا: الكمبوننتات خارج <router-outlet> (مثل Navbar).

وبعد م يتم انشاءها بيبتدي يتعمل نيكيست ل  
this.router.events 
و ب التالي اي كمبوننت عامل سبسكرايب ليه م اللي تم انشاءها بياخد القيمة اللي اتعملها نيكست ف this.router.events 

وبعد كده يتم انشاء باقي الكمبوننانتس ال بتنتمي لل راوتر اوت ليت 
------
2- 
لما الراوتر لينك يتغير بيتعمل نيكست ل this.router.events 
و ب التالي الكمبونانتس الحيه وقت م الراوتر لنك بيتغير  وبتفضل موجوده وحيه بعد م الراوتر لينك بيتغير
يعني مش بيحصلها ديستوري اللي بتعمل سبسكرايب ليه بيتبعتلها القيمة دي فورا 
*/