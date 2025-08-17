import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { IAddProduct, IProduct, IUpdateProduct } from '../../models/iproduct';
import { ICategory } from '../../models/icategory';
import { environment } from '../../../environments/environment.development';
import { BehaviorSubject, catchError, map, Observable, of, Subject, Subscription } from 'rxjs';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { EditProduct } from '../edit-product/edit-product';
import { ILogin, ILoginValue, IRegister, IRegisterValue } from '../../models/Register';

@Injectable({
  providedIn: 'root'
})
export class ProductService{

  // ace:{
  //         x?:string,
  //         y?:number,
  //         z?:{
  //               x:string,
  //               y:number
  //         }
  //         c?:{
  //           [h:string]:string|string[]
  //         }
  // }={
  //     c:{
  //       ahmed:"alaa"
  //     },
  //     x:"ahmed",
  //     z:{
  //       x:"al",
  //       y:100
  //     }
  // }


  // استقبل اي حاجة جايه م الداتا بيز هنا ي معلم ومتعملهاش ريتيرن تاني 

 private updatedApiProductsITI=new BehaviorSubject<IProduct[]>([]);
   $updatedApiProductsITI=this.updatedApiProductsITI.asObservable();

private updatedCategoryProductsITI=new BehaviorSubject<ICategory[]>([]);
   $updatedCategoryProductsITI=this.updatedCategoryProductsITI.asObservable();


  private isDeleted=new BehaviorSubject<boolean>(false);
   $isDeleted=this.isDeleted.asObservable();


  private catId=new BehaviorSubject<number|null>(null);
   $catId=this.catId.asObservable();

  private search=new BehaviorSubject<string|null>(null);
   $search=this.search.asObservable();

      private isConnect=new BehaviorSubject<boolean>(true);
      $isConnect=this.isConnect.asObservable();

  // x:[string,number][] // كده ارري من كي فاليو بير 
  // = [  
  //     ["",5],["",6]
  //   ];
  // z: {
  //     // كده ال z عبارة عن انديكسر
  //   [header:string]: string|string[];
  //   //"أي مفتاح نصّي (string) تحطه في الكائن ده، القيمة اللي عليه لازم تكون إما string أو string[]."
  //   [header: number]: string | string[];
  //     //"أي مفتاح عددي (number) يتحط في الكائن، لازم تكون قيمته من نوع string أو string[]."
  //   }={};

   constructor(private router:Router,private httpClient:HttpClient) {
  //  this.ace;
  //  debugger;
  //  this.ace!.c!["ahmed"]="kamal";
  //  debugger;
  //   const  clone={...this.ace}; //  كده عملت نسخة مستقلة علشان متغيرش ف الاصل 
  //   clone.c={}
  //   this.ace;
  //    debugger;
  //   debugger;
  //       console.log( this.ace.c); // اتوقع هتبقي اوبجيكت فيه   ahmed:"alaa" // برافو توقعك صح 
  //   this.ace.c={
  //     "a":"b","b":"a"
  //   }
  //   console.log( this.ace.c); // اتوقع هتبقي اوبجيكت فيه a:"b"  b:"a" // برافو توقعك صح 

    // this.z={
    //   "aa":""
    // }   ;
  //  console.log(this.z["aa"]);
  }

      GetCategoriesInProducts()
      {
      //  var z: {
      //     [header: string]: string | string[];
      //     [header: number]: string | string[];
      //   };
          //  debugger;


  const Token = localStorage.getItem('AngularToken')||sessionStorage.getItem('AngularToken');
      if(!Token)
        {
          return;
        }
             this.httpClient.get<ICategory[]>(`${environment.baseUrl}/api/ITIProduct/categoriesInProducts`)
             .subscribe(
              {
                  next:  value=>
              {
                          //  debugger;

                this.updatedCategoryProductsITI.next(value);
              },
               error: () => {
            this.connectError();
               
          }
        })
        // debugger;
      }

   isProDeleted(bool:boolean){
      this.isDeleted.next(true);

  }
 setSearch(value:string|null){
      this.search.next(value);
  }
  setCatId(id:number|null){
      this.catId.next(id);
  }
  // x({ edit, xxx }: { edit: EditProduct; xxx: number })
  // {
      
  // }
  GetITIProducts(par?:{ SortBy?:string,categoryId?:number,name?:string,category?:string})//  هيحصلها فاير اول م يجيب العناصر م الباك اند ولما يحصلها فير هتشوف اللي عاملها سبسكرايب وهتديله القيم  // ببعت بار من نوع اوبجيكت فيه العناصر دي
  {
    // debugger;
    if(!par)
      par={}  // لو البار ان ديفايند خليها ب اوبجيكت فاضي 
         
    // debugger;
    console.log("start GetITIProducts")
    let params=new HttpParams(); //HttpParams كائن غير قابل للتعديل (immutable)، يعني params.set(...) لا يغير الكائن الأصلي، بل يرجّع نسخة جديدة معدّلة.
  // params.appendAll({
  //   "x":"","z":""
  // })

    if (par.SortBy == undefined) {
      par.SortBy = "priceDesc";
    }
    
                      //set(param: string, value: string | number | boolean): HttpParams; // بتعمل ريتيرن ل HttpParams فيه البارام والفاليو اللي انا حطيتها  
        params = params.set('SortBy', par.SortBy);
      
 //set بيضيف كل مفتاح بقيمته، ولو المفتاح اتكرر بيستبدل قيمته، لكن هنا كل مفتاح مختلف (name و categoryId) فالاتنين بيتضافوا.
      if(par?.categoryId)
         params=params.set('categoryId',par.categoryId)
       if(par?.name)
         params=params.set('name',par.name)
        if(par?.category)
         params=params.set('category',par.category)   
// debugger;//Dont Wanna Talk..>>
      this.httpClient.get<IProduct[]>(`${environment.baseUrl}/api/ITIProduct?`,{
        params:params
      })
      .subscribe(
        //اي حاجة اوبسرفابول لو مفيش حاجة عاملة عليها سبسكرايب مش هتتنفذ
        /*
            لأن Observables في RxJS "كسولة" (Lazy)
            يعني:
            مش بتشتغل لوحدها
            لازم حد "يشترك" فيها (subscribe) علشان تبدأ تشتغل وتصدر بيانات
        */
        value=>
          {this.updatedApiProductsITI.next(value);
            // debugger;
            // this.router.navigate(['/totalOrder']);


          }
      )
     
     
     
     ;
   }

   GetITICategories(par?:{SortBy?:string,category?:string})
   {
     let params=new HttpParams();
     if(par?.SortBy)
        params=params.set('SortBy',par.SortBy)
        if(par?.category)
         params=params.set('category',par.category)  

        return this.httpClient.get<ICategory[]>(`${environment.baseUrl}/api/ITIProduct/categories?${params}`);

        // دي عبارة عن اوبسيرفابول get : Observable<...>
        // وهيحصلها نيكست لما تجيب الداتا من ال API 
        // وبعد م تجيب الداتا من ال API  هتشوف اللي عامل سبسكرايب عليها وهتبعتله القيمة 
        // وبيحصلها نيكست بعد اول لفة بتلف فيها ع الكمبونانتس بتروح تكلم الباك وتجيب الداتا منه ويتتعملها نيكست 
        // ده اللي بيحصل 
 
   }

  tryConnect() {
      return this.httpClient.get<ICategory>(`${environment.baseUrl}/api/ITIProduct/category/${1}`)  // 🔁 حط أي endpoint بسيط هنا
        .pipe(
          map(() => true),            // ✅ لو نجح الاتصال // بتعمل ماب من ال داتا ل ترو وهترجع اوبسيرفر من ترو 
          catchError(() => of(false)) // ❌ لو فشل الاتصال
          //(err: any, caught: Observable<boolean>) => Observable<boolean> //of(false)=>معناها: Observable يحتوي على القيمة false
        );
    }


    // https://localhost:7296/api/ITIProduct/categoriesInProducts

 
   connectError()
   {
    this.isConnect.next(false);
   }

   getProductById(id:number)
   {
     return this.httpClient.get<IProduct>(`${environment.baseUrl}/api/ITIProduct/${id}`)
   }

   AddProduct(product:IAddProduct)
   {

    

    const formData = new FormData(); //بما إنك كاتب في الـ IAddProduct إن pictureUrl: File، يعني المستخدم بيرفع صورة فعلية من <input type="file">، فـ لازم تبعت البيانات باستخدام FormData.
    


    
     // x:[string,number][] // كده ارري من كي فاليو بير بحيث ان الكي استرنج والفاليو بير نامبر 
    // = [  
    //     ["",5],["",6]
    //   ];    
      /*
      var FormData: new (form?: HTMLFormElement, submitter?: HTMLElement | null) => FormData
      interface FormData {
        [Symbol.iterator](): FormDataIterator<[string, FormDataEntryValue]>; 
        // الكلاس اللي يبعمل امبليمنت لل انترفيس ده بيطبق ميثود اسمها  [Symbol.iterator]()
        // والميثود دي بترجع حاجة من نوع  FormDataIterator<[string, FormDataEntryValue]>;  اللي بتعمل امبليمنت ل Iterator<[string, FormDataEntryValue]>
        // ف اي كلاس بيتخدم الميثود [Symbol.iterator]() وبيرجع حاجة من نوع Iterator ف بكده الكلاس ده بيتعامل ك انه كائن قابل للتكرار
       // وده السبب إن الكائنات زي FormData و Map و Set نقدر نستخدم معاها for...of رغم إنها مش Arrays.
        عاملك مثال عليها ف كلاس باك اند ايرور
        */ 
      formData.append('name', product.name); // بعمل اببيند لل اري اللي عبارة عن ارري من كي فاليو بير بضيف فيها قيمة من نوع كي فاليو بير بحيث ان ال فاليو نيم وال كي فاليو بير برودكت دوت نيم 
      formData.append('price', product.price.toString());
      formData.append('categoryId', product.categoryId.toString());
      formData.append('quantity', product.quantity.toString());
      formData.append('pictureUrl', product.pictureFile); // لازم تكون الصورة ملف (File)

        this.httpClient.post<IAddProduct>(`${environment.baseUrl}/api/ITIProduct`,formData).subscribe(
              // x=>... // بستدعي الميثود سبسكرايب ولما بستدعيها بيدخل جواها طبعت وبعدين لما بيجيب الداتا من ال اي بي اي بيقول جوه الميثود عنده يعمل انفوك ويبعتلي ال قيمة اللي جابها من ال اي بي اي وبعدين لما يعمل انفوك بييجي هنا بعد دي => وبعمل ال منطق بتاعي بقي اللي انا عاوزة 
              {
                next:(value)=>
                {
                                // debugger;//مفروض يدخل هنا الاول
        
                  console.log("Product Added Successfully", value);
                  // alert('added successfully');
                this.GetITIProducts();
                 this.GetCategoriesInProducts();
                  
        
                  Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: "Product Added Successfully",
                    showConfirmButton: false,
                    timer: 1500
                  });
              
        
        
                  
        
                  
                    // علشان اللي عامل سبسكرايب عليه يتبعتله القيمة 
                
          // this.GetCategoriesInProducts()
          //          .subscribe(
          //           ()=>
          //           {
          // //             debugger;//مفروض يدخل هنا التاني
          // //  console.log();
          //             this.router.navigate(['/totalOrder'])
          //           }
                   
          //          );
        
        
        
                   // عاوزة يروح يجيب القيم م الداتا بيز تاني علشان اللي بيعملوا سبسكرايب عليه يتبعتلهم القيمة 
              
                }
                
                ,
                error:()=>
                {
                  // alert('failed to Add Products!');
                  Swal.fire({
                  icon: "error",
                  title: "Oops...",
                  text: "failed to Add Products!",
                });
                }
              }
            );
   }

     DeleteProduct(id:number)
   {
      this.httpClient.delete<IProduct>(`${environment.baseUrl}/api/ITIProduct/${id}`).subscribe
      (
          {
             next:(value)=>
                    {
                      console.log("start delete")
                      this.isProDeleted(true);
                      console.log("Product Deleted Successfully", value);
                      // alert('added successfully');
            
            
                      Swal.fire({
                        position: "top-end",
                        icon: "success",
                        title: "Product Deleted Successfully",
                        showConfirmButton: false,
                        timer: 1500
                      }); 
                        // this.filterProduct = this.filterProduct.filter(p => p.id !== product_id); // بعد م يمسحها هيحدث العناصر اللي بتتعرض بحيث انه اللعناصر اللي هتتعرض تكون ال اي دي بتاعها مش بيساوي ال اي دي بتاع البرودكت اللي انا مسحته 
      // alert('aaa');
                         this.GetITIProducts(); // اما تجيب الفاليو تنفذ دي
                         this.GetCategoriesInProducts();
                    }
                    
                    ,
                    error:(err)=>
                    {
                      // alert('failed to Add Products!');
                      Swal.fire({
                      icon: "error",
                      title: "Oops...",
                      text: "failed to Delete Products!",
                    });
                     console.log("Error Occured", err)
                    }
                    
          }
      
      )
      //  this.GetITIProducts();
   }

   updateProduct(updateProduct:IUpdateProduct)
   {
      const Data=new FormData();
      // Data.append('id',updateProduct.ProductId.toString());
      Data.append('name',updateProduct.name);
      Data.append('categoryId',updateProduct.categoryId.toString());
      Data.append('price',updateProduct.price.toString());
      Data.append('quantity',updateProduct.quantity.toString());
      if(updateProduct.pictureFile)
      {
        Data.append('pictureUrl',updateProduct.pictureFile);
      }

    
      const value=this.httpClient.put<IProduct>(`${environment.baseUrl}/api/ITIProduct/${updateProduct.ProductId}`,Data);
      value.subscribe(
        // عاوز بعد م اعمل ابديت اروج اجيب الداتا من الداتا بيز 
        ()=>   
        this.GetITIProducts()
       );
       return value;
 
  // this.x()+4;
   }


  // x():number  // معناها ان الريتيرن بتاع الميثود دي نامبر x() يعني الاكسكيوت بتاع الميثود هيبقي نامبر
  // {
  //     return 5;
  // }




register(register:IRegister)
{
 return this.httpClient.post<IRegisterValue> // هيرجعلي ي استرنج ي بولين 
  (
    `${environment.baseUrl}/api/AccountITI/`,register
  )
}
login(login:ILogin)
{
  return this.httpClient.post<ILoginValue> // هيرجعلي ي استرنج ي بولين 
  (
    `${environment.baseUrl}/api/AccountITI/login`,login
  )
}

// اعمل اللوجين 

  }

 
/*
      السبب بسيط: httpClient.delete() بيرجع Observable، يعني العملية غير متزامنة (asynchronous).

فلما تكتب:

ts
نسخ
تحرير
this.httpClient.delete(...).subscribe(...);
this.GetITIProducts();
اللي بيحصل فعليًا:

Angular يبدأ ينفذ http.delete(...) → ده Observable مش بيبدأ فعليًا غير لما توصله subscribe ✅

فورًا بعدها ينفذ this.GetITIProducts(); — وده يتنفذ فورًا قبل ما الـ delete يخلص 😓

بعد ثواني قليلة، subscribe اللي جوه delete يشتغل، لكن ساعتها انت كنت بالفعل جبت الداتا القديمة!

🎯 الحل الصح
لازم تحط this.GetITIProducts() جوه الـ subscribe → بعد ما المسح يخلص بنجاح:

*/

/*
        سؤالك في الصميم! 👏 وخليني أقولك إنك قربت تلمّ المنظومة كلها، بس محتاج نوضح الفرق بين:

✅ Observable بيرجع داتا جديدة كل مرة يتطلب فيها
🆚
✅ Observable بيبث بشكل مركزي ومستمر لجميع المشتركين

🧠 الفرق الجوهري بين النوعين:
✳️ النوع الأول: "Cold Observable" (زي GetITIProducts())
ده اللي بيشتغل بس لما حد يعمل له subscribe.
وكل مرة subscribe عليه = request جديد بيحصل (زي http.get()):

ts
نسخ
تحرير
this.http.get(...) // Cold Observable
يعني كل كومبوننت يشترك عليه = نداء جديد للـ API

مفيش بث تلقائي على أي حد تاني

مفيش تخزين للقيمة الأخيرة

✳️ النوع التاني: "Hot Observable" (زي route.events, BehaviorSubject, Subject, ReplaySubject)
ده بيكون شغال ويبث القيم طول الوقت، وأي كومبوننت يشترك عليه بيشوف القيم اللي بتتبث.

مثال:

ts
نسخ
تحرير
this.router.events.subscribe(...) // Hot Observable
أو:

ts
نسخ
تحرير
private _products = new BehaviorSubject<IProduct[]>([]);
public products$ = this._products.asObservable();
أول ما يحصل .next(...) عليه → كل المشتركين بياخدوا القيمة

أي حد جديد يشترك على BehaviorSubject بياخد آخر قيمة تلقائيًا

هو ده اللي محتاجه علشان تخلي البث مركزي لجميع الكومبوننتس

🤯 ليه route.events Hot بالرغم إنه Observable؟
لأنه بيبث داخليًا من جوه الـ Angular نفسه.
هو عبارة عن Subject بيبث كل الأحداث اللي بتحصل على مستوى الـ Router.

فـ:

ts
نسخ
تحرير
this.router.events.subscribe(...) // بث مستمر (Hot)
✅ الحل العملي ليك:
لو عاوز دالة زي GetITIProducts() تبث تلقائيًا وتشتغل زي router.events:

استخدم BehaviorSubject داخل الـ service.

خزن فيه الداتا من http.get(...).

خليه مصدر البيانات الوحيد لبقية الكومبوننتات.

💡 مثال مختصر يربط الدنيا:
ts
نسخ
تحرير
// product.service.ts
private _products$ = new BehaviorSubject<IProduct[]>([]);
public products$ = this._products$.asObservable();

GetITIProducts() {
  this.http.get<IProduct[]>(...).subscribe(data => {
    this._products$.next(data);
  });
}
ts
نسخ
تحرير
// product-list.component.ts
this.productService.products$.subscribe(data => {
  this.products = data;
});
🧩 ملخص سريع:
السلوك	Cold Observable	Hot Observable
بث تلقائي؟	❌	✅
يشتغل عند subscribe؟	✅	✅
يخزن آخر قيمة؟	❌	✅ (لو BehaviorSubject)


اعرف ازاي اذا كانت هوت ولا كولد ! 
ويعني اي دي ؟
يخزن آخر قيمة؟	❌	✅ (لو BehaviorSubject)
مش فاهم 


قال ChatGPT:
سؤالك ممتاز، وده موضوع جوهري في RxJS وAngular، فخليني أشرح لك بهدوء وتفصيل:

✅ أولًا: يعني إيه "يخزن آخر قيمة"؟
يعني الـ Observable يفتكر آخر قيمة اتبعت، ولما أي مشترك جديد يعمل subscribe:

❌ لو ما بيخزنش آخر قيمة (زي Subject العادي أو http.get)
→ المشترك الجديد مش هيشوف أي قيمة، إلا لو جات قيمة جديدة بعد ما اشترك.

✅ لو بيخزن آخر قيمة (زي BehaviorSubject)
→ المشترك الجديد هياخد فورًا آخر قيمة كانت موجودة، حتى لو اتبعتت قبل اشتراكه.

🧠 مثال:
ts
نسخ
تحرير
const subject = new Subject<number>();
subject.next(10);
subject.subscribe(x => console.log('Subscriber 1:', x)); // مش هيطبع حاجة
subject.next(20); // Subscriber 1: 20
لكن لو BehaviorSubject:

ts
نسخ
تحرير
const subject = new BehaviorSubject<number>(0);
subject.next(10);
subject.subscribe(x => console.log('Subscriber 1:', x)); // Subscriber 1: 10
✅ ثانيًا: أعرف منين إذا كانت Hot ولا Cold؟
✳️ 1. Cold Observable (البارد)
هو اللي بيبدأ تشغيله فقط لما تعمل عليه subscribe.

وأي subscribe جديد = تشغيل جديد مستقل.

مثال مشهور: http.get(...) أو of(...) أو from(...).

ts
نسخ
تحرير
const obs$ = this.http.get(...); // Cold
العلامات:

مفيش بث مركزي.

كل مرة بـ subscribe بتبدأ من أول وجديد.

لا يحتفظ بالقيمة الأخيرة.

✳️ 2. Hot Observable (السخن)
هو اللي شغّال أصلاً في الخلفية، وبيبث القيم باستمرار.

أي مشترك جديد بياخد القيمة الحالية أو التالية.

مثال: BehaviorSubject, Subject, ReplaySubject, router.events.

ts
نسخ
تحرير
const subj = new BehaviorSubject(0); // Hot
العلامات:

بيبث للجميع في نفس الوقت.

لو اشتركت متأخر تشوف القيمة الحالية.

يشتغل حتى بدون اشتراك (لو حد عمله next).



*/