import { HttpEvent, HttpEventType, HttpHandlerFn, HttpHeaders, HttpInterceptorFn, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { Product } from '../components/product/product';

// const x:string = "";
//type HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => Observable<HttpEvent<unknown>>;
// هساويها ب فانكشن 

// دي فانكشن من نوع HttpInterceptorFn بتاخد:
// 1- req: الكائن اللي بيمثل طلب HTTP
// 2- next: دالة بتمرر الطلب للمراحل التالية في السلسلة
// وبترجع Observable من نوع HttpEvent
// عملت لها export علشان أقدر أضيفها في مزودات (providers) وتبقى Interceptor معترف بيه في Angular
// Angular بيستدعي الفانكشن دي تلقائي عند كل طلب HTTP، وبيبعت req و next كوسائط، وأنا بتصرف جواها زي ما عاوز.

//(req: HttpRequest<unknown>, next: HttpHandlerFn) => Observable<HttpEvent<unknown>>;
export const handleReguestInterceptor: HttpInterceptorFn = (req, next) => {
// ف ال TS لو فيه ديليجيت بياخد براميترز مثلا 3 ممكن مبعتش حاجة منهم او ابعت واحد منهم او اتنين او تلاثه ولازم ارجع ال ريتيرن اللي هو بيرجعه وممكن ابعت اكتر من بارميتر وبيتجاهل الباقي وممكن ابعت اقل او مبعتش وبيخليهم ان ديفايند
// انما ف ال سي شارب لازم ابعت البارميترز كلها وارجع الريتيرن 

  // type HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => Observable<HttpEvent<unknown>>;
  // ((value: IProduct[]) => void)           in Ts  الصيغة ف ال تي اس 
  // = Action<IProduct[]> value              in C#  تقابلها هذه الصيغة في ال سي شارب
  //(req: HttpRequest<unknown>, next: HttpHandlerFn) => Observable<HttpEvent<unknown>> in Ts  الصيغة ف ال تي اس 
  //=Func< HttpRequest<unknown>, HttpHandlerFn,Observable<HttpEvent<unknown>>        in C# تقابلها هذه الصيغة في ال سي شارب
    //  (req:HttpRequest<unknown>,next: HttpHandlerFn) => Observable<HttpEvent<unknown>> ;
  // هضيف ال توكن للهيدر 
  const Token = localStorage.getItem('AngularToken')||sessionStorage.getItem('AngularToken');
  // هاخد نسخة من الريكويست 


  let reqHeadrer = req.headers;
  if (Token != null) {
   reqHeadrer= reqHeadrer.set("Authorization",`Bearer ${Token}`) //     * @returns A clone of the HTTP headers object with the given value deleted.  // بيمسح القيمة القديمة ويضيف الجديدة لو لقي نفس ال نيم  // اعمل كده عادي من غير م اعمل كده //   reqHeadrer=reqHeadrer.set("Authorization",token)  لاني لما اعمل سيت تحت هتغير ف النسخة اللي فوق كمان لانها كلاس والكلاس ريفرنس تايب 
  }

  // if(req.method=="POST")
  // {
  //   reqHeadrer=reqHeadrer.append("lan","egy"); // بيضيف ع القديمة لو لقي نفس ال نيم  //@returns — A clone of the HTTP headers object with the value appended to the given header.
  // }
  var cloneRequest = req.clone( // نسخة من الاصل
    {
      headers: reqHeadrer
    }
  )
  // عاوز امسك ال ريسبونس اللي راجع ولو 200 اعمل لوجيت معين 
  //Observable<HttpEvent<unknown>> يعني لو عملت عليها سبسكرايب هييجي هنا ب الريسبونس 
  
  /*
        
        مش محتاج اعمل سبسكرايب ب نفسي لان ال providers هو اللي بيعمل سبسكرايب بنفسه 
        عن طريق     ,provideHttpClient(withFetch(),withInterceptors([handleReguestInterceptor]))
          
  */
  
  return next(cloneRequest).pipe(
    tap(
     // HttpEvent<unknown> //type HttpEvent<T> = HttpSentEvent | HttpHeaderResponse | HttpResponse<T> | HttpProgressEvent | HttpUserEvent<T>;

      response => {
        if (response instanceof HttpResponse) {
          if (response.status === 200) {
            console.log('status is 200');
          }
        }

      }
    )

  );

}

// ✅ الخلاصة:


// as HttpResponse<unknown>	وقت الترجمة (TypeScript)	✅ لازم	لأن HttpResponse generic
// instanceof HttpResponse	وقت التشغيل (JavaScript)	❌ ملوش لازمة	لأن generic مش موجود أصلاً في runtime


//HttpInterceptorFn
// ممكن اعمل كده
// export const handleReguestInterceptor: HttpInterceptorFn=x;
// function x (req: HttpRequest<unknown>, next: HttpHandlerFn)
// {
//     return  next(req);
// }
/*
هي معمولة كده ف ب التالي بترجعلي نسخة جديدة ومش بتغير ف الاصل 

set(name: string, value: string | string[]): HttpHeaders {
  // ينشئ نسخة جديدة من الكائن الحالي مع القيمة المحدثة
  const clone = this.clone();
  clone.headersMap[name.toLowerCase()] = Array.isArray(value) ? value : [value];
  return clone;
}

مش كده 

set(name: string, value: string | string[]): HttpHeaders {
  this.headersMap[name.toLowerCase()] = Array.isArray(value) ? value : [value]; 
  return this; 
}


*/