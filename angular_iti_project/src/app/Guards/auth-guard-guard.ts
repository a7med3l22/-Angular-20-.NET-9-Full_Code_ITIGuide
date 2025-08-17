import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Register } from '../components/service/register';
import { combineLatest, map } from 'rxjs';
import { ToastrService } from 'ngx-toastr';


export const authGuardGuard: CanActivateFn = (x,y) => {


// type Params = {  // ده نوع ال X
// [key: string]: any;
// };
// debugger;
// var idParam=x.params['id'];
// var includeId=y.url.includes('details');
// if(includeId&&idParam==1)
// {
// console.log('idParam Is',idParam);
// }else{
//   return false;
// }
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



  // this.router.events اول م يحصل نافيجيشن بيعمل ليسن هنا قبل الليسن بتاعت 
  // واول م يلاقيه رايح للهوم بيبتدي يدخل هنا 
  var _register=inject(Register);
   var _router=inject(Router);



 return combineLatest([   
    _register.$isLogin,
    _register.$isHaveLocalHost
  ]).pipe(
    //project: (value: [boolean, boolean], index: number) => GuardResult
    map(([isLogin,isHaveLocalHost])=>
      
      {
        // debugger;
        console.log("isLogin:"+isLogin) 
        console.log("isHaveLocalHost:"+isHaveLocalHost)
        // return isLogin||isHaveLocalHost;
        if(isLogin||isHaveLocalHost)
        {
          return true;
        }
        else{
            _router.navigate(['/login']);
            return false;
        }
      }
    
    
    ) // الفاليو ال ف ال ابوسيرفابول داخلة [boolean,boolean] , وخرجتها boolean
  );
   // هترجع اوبسيرفابول من بولين // وانتا ممكن ترجع بولين او اوبسيرفابول اوف بولين  
};
