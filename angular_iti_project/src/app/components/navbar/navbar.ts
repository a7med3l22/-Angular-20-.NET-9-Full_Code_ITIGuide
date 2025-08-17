import { Component, EventEmitter, input, Input, OnChanges, OnDestroy, OnInit, Output, output, SimpleChanges, viewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { Register } from '../service/register';
import { filter, Observable, Subscription } from 'rxjs';
import { ProductService } from '../service/product-service';
import { Store } from '@ngrx/store';
import { selectCurrentLanguage } from '../../language/store/language.selectors';
import { langAction } from '../../language/store/language.actions';
import { langReducer } from '../../language/store/language.reducer';
import { FormsModule } from '@angular/Forms';

@Component({
  selector: 'app-navbar',
  imports: [FormsModule,CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit, OnDestroy {

  // @Output() OnSearch: EventEmitter<string> = new EventEmitter<string>();
  isAr:boolean=false;
  Subscription:Subscription=new Subscription();
  APISearch:string|null=null;
  IsincluetotalOrderInUrl!: boolean;
  isOrderPage!: boolean;
  isLogin: boolean = false;        // تمام
  isFindLocalHost!: boolean;     // زي الفل 
  currentLang$: Observable<string>;
  //  localHostSubscription!:Subscription;
  constructor(private store:Store,private productService:ProductService, private router: Router, private route: ActivatedRoute, private _register: Register) {
    // this.isFindLocalHost=_register.isFindLocalHost();
  
      this.currentLang$ =  this.store.select(selectCurrentLanguage)
       this.Subscription.add( this.store.select(selectCurrentLanguage).subscribe(
          value=>
          {
            if(value=='ar')
            {
              this.isAr=true;
            }else{
              this.isAr=false;
            }
          }
      ))

  }
OnChangeSearch(event:Event) {
//   debugger;
//  event.data
  const value = (event.target as HTMLInputElement).value;
// debugger;
  this.APISearch = value;
  this.productService.setSearch(value);
}

  ngOnDestroy(): void {
    this.Subscription.unsubscribe();
  }
  ngOnInit() {

       this.productService.GetCategoriesInProducts();  // حطتها هنا علشان يروح يجيب ال كاتيجوريز كلها ف اول دخول ليه هنا ويحفظها ف ال بيهيفيور سابجيكت اللي انا بعمل سبسكرايب عليه من الكمبونانتس اللي بحتاج فيها الكاتيجوري 


      // this.prodectService.$search
      


    // debugger;
    console.log('Enter Inside:- ngOnInit Navbar');
    this._register.$isHaveLocalHost.subscribe(value => {
      console.log('1-Enter Inside:- $isHaveLocalHost subscribe');

      // debugger;

      this.isFindLocalHost = value;
      // console.log('this.isFindLocalHost سبسكرايب ال لوكال هوست ,'+this.isFindLocalHost);
    }
    );



    /*
    مهم اوي عاش
            لما عدي ع دي 
      this.router.events.pipe(
          filter((value):value is NavigationEnd =>value instanceof NavigationEnd)
          ).subscribe(
            value=> );
      ف اول لفة عرف اني عملت سبسكرايب علي ال راوتر ايفينتس بس مدخلش جواها لان مكانش ال ايفينت موجود ف لما لاليفينت بقي موجود دخل ف سبسكرايب لانه خلاص عرف اني عملت سبسكرايب ع الايفينت من اول لفه 

            
    
    */
    this.router.events.pipe(
      filter((value): value is NavigationEnd => value instanceof NavigationEnd)
    ).subscribe(
      value => {
        console.log('2-Enter Inside:- router subscribe');

        // debugger;


        this._register.isFindLocalHost();   // كل اما يدخل جوه هنا بتتنفذ وطبعا انا عرفت بيدخل كل مرة هنا امتا راجع التعليق اللي ف الراوتس 


        // console.log('سبسكرايب ال يو ار ال , this.isFindLocalHost'+this.isFindLocalHost);






        this.IsincluetotalOrderInUrl = value.url.toLowerCase().includes('totalorder') || value.url === '' || value.url === '/' || value.url === '#' || value.url.includes('#');
        //  console.log("جوه سبسكرايب  "+this.IsincluetotalOrderInUrl); //هتطبع قيمتها لانها جوه السبسكرايب هتكون اتهيأت لانها بتتهيأ مش من اللفه الاولي 
        //           debugger;

        if (this.IsincluetotalOrderInUrl) {
          if (!this.isLogin && !this.isFindLocalHost)//this.isLogin هتساوي ترو ف حالة اني ضغطت ع زرار اللوجين فقط غير كده ب فولس
          {
            this.router.navigate(['/login']);
          }
          else {
            // debugger;
            this.isOrderPage = true;
          }
        } else {
          this.isOrderPage = false;

        }

      }

    );
    // console.log("برة سبسكرايب    "+this.IsincluetotalOrderInUrl); // مش هتطبع قيمتها لانها هتتحسب عند اول لفه هنا واول لفه هنا هتبقي ان ديفايند 

    this.Subscription.add( this._register.$isLogin.subscribe(bool => {
      console.log('3-Enter Inside:- $isLogin subscribe');

      this.isLogin = bool
    }));
  }


  // setSearch(search: string) {

  //   this._searchService.setSearch(search);

  // }



  // عاوز كل اما قيمة search تتغير ابعتها للأب 
  // fireSearch(value: string) {
  //   this.OnSearch.emit(value); // يعني هينفذ (OnSearch) 
  // }

  // login()
  // {
  //   this._register.logIn();
  // }
  logout() {
    this._register.logOut();
    // this.isFindLocalHost=false;
  }
  // goToOrder()
  // {
  //   if(this.isLogin)
  //   {
  //     this.router.navigate(['/totalOrder']);

  //   }
  // }
      
changeLang()
{
  //select in store
  //(mapFn: (state: object) => string)
  //const selectCurrentLanguage: MemoizedSelector<object, string, (s1: IlanguageState) => string>


debugger;
  this.store.dispatch(langAction(this.isAr?
    {lang:'en'}
    :
    {lang:'ar'}
  ))
}
}

/*
this.router.events.pipe(
  filter((value): value is NavigationEnd => value instanceof NavigationEnd)
).subscribe(
  value => this.IsincluetotalOrderInUrl = value.url.includes('totalOrder')
);
1️⃣ ما الذي يفعله this.router.events؟
this.router.events عبارة عن Observable يبعث كل الأحداث التي تحدث داخل Router.

هذه الأحداث تشمل:

NavigationStart

NavigationEnd

NavigationCancel

NavigationError

... إلخ.

2️⃣ ما الذي يفعله filter(...) هنا؟
ts
نسخ
تحرير
filter((value): value is NavigationEnd => value instanceof NavigationEnd)
يقوم بتصفية الأحداث بحيث:
✅ يمرر فقط الأحداث التي تكون من النوع NavigationEnd.
❌ يهمل باقي الأحداث مثل NavigationStart, NavigationCancel, NavigationError.

3️⃣ ما الذي يحدث عند عدم وجود NavigationEnd؟
عند:

عمل Refresh.

أو كتابة نفس الـ URL والضغط Enter.

🔹 لا يحصل Navigation جديد داخل Angular.
🔹 وبالتالي، لا يقوم Router بإرسال أي حدث.
🔹 وبالتالي:

الـ filter(...) لا يجد أي حدث يمرره.

الـ subscribe(...) لا يُنفذ إطلاقًا.

⚠️ النتيجة:

ts
نسخ
تحرير
value => this.IsincluetotalOrderInUrl = value.url.includes('totalOrder')
❌ لن يتم تنفيذه إطلاقًا، وستظل IsincluetotalOrderInUrl:

إما بقيمتها undefined إذا لم تكن مهيئة.

أو تبقى على آخر قيمة قديمة إن كانت مهيئة من قبل.



*/