import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild, viewChild } from '@angular/core';
import { Product } from "../product/product";
import { CommonModule } from '@angular/common';
import { ChangeColor } from '../../directives/change-color';
import { FormsModule } from '@angular/Forms';
import { ICategory } from '../../models/icategory';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { BehaviorSubject, concatWith, filter, Subscription } from 'rxjs';
import { ProductService } from '../service/product-service';
import { IProduct } from '../../models/iproduct';
@Component({
  selector: 'app-order',
  imports: [Product, CommonModule, FormsModule],
  templateUrl: './order.html',
  styleUrl: './order.css'
})
// اللي هعمله ك الاتي عاوز اجيب ال كاتيجوريس اللي ال برودكتس مرتبطة بيها ههندل ده من الباك وعاوز استدعيها ف الفرونت كل م اضيف برودكت او احذف برودكت من الفرونت 
export class Order implements AfterViewInit, OnDestroy, OnInit {
  @Input() searchValue: string = '';
  categories!: ICategory[];
  filteredCategories!: ICategory[];
  //  private value=new BehaviorSubject<number|undefined>(undefined); // عاوزها اول م تتغير تنادي دي  productService.setCaiId(this.value)
  //  $value=this.value.asObservable();
  value: number | null | undefined = null;

  /*
      [ngModel]="$value| async"    لو القيمة اتغيرت ف ال TS هتتغير ف ال HTML وعملتها async  لأن:

    $value هو Observable، مش قيمة مباشرة
      
      
     (ngModelChange)="OnChangeValue($event)"  لو القيمة اتغيرت ف ال html هتتغير ف ال Ts

*/

  // ngOnInit(): void {
  //   this.$value.subscribe(
  //     value=>this.productService.setCatId(value)

  //   )
  // }



  OnChangeValue(value: number) {


    // debugger;
    this.value = value; // علشان لو حبيت استخدم القيمة جوه الكمبوننت تبقي متحدثه من ال html 
    this.productService.setCatId(value);
    // this.value.next(value);
  }

  //  private catId=new BehaviorSubject<number|undefined>(undefined);
  //   $catId=this.catId.asObservable();
  isEmpty!: boolean;
  subscription = new Subscription;
  // set TotalOrderprice(value:number)
  // {
  //   // this.OnTotalOrderpriceChanged.emit(value);
  // }

  //TotalOrderprice:number=0; // عاوز لما قيمتها تتغير ابعتها للأب
  // @Output() OnTotalOrderpriceChanged=new EventEmitter<number>();
  @ViewChild('input') myInput!: ElementRef<HTMLInputElement>; //'input' is ElementRef of HTMLInputElement 
  @ViewChild(Product) catchProduct!: Product;
  constructor(private router: Router, private productService: ProductService)
   {


    //  this.productService.GetCategoriesInProducts();
  }

  ngOnInit(): void {

this.subscription.add(
      this.productService.$catId.subscribe(

        value=>
        {
             this.value=value;
        }
      ));




      this.productService.GetCategoriesInProducts(); // هيخرج م الميثود وييجي هنا 
     this.productService.$updatedCategoryProductsITI.subscribe( // هنا بعمل سبسكرايب علي ال بيهيفيور سابجيكت ف هتدخل هنا فورا وتاخد اخر فاليو موجوده فيها سواء كانت الانشيال او لا 

      value => { // هياخد اخر قيمة موجوده ف ال updatedCategoryProductsITI سواء كانت الانشيال او اي قيمة اي كانت اي هي 
          //  debugger; // المفروض يدخل هنا علشان يحدث القايمة 
       
       

           this.filteredCategories = value;
      }
    )


    this.subscription.add(
      this.productService.GetITICategories().subscribe(
        {
          next: value => {
            // debugger; ////////Finally
            this.categories = value;
          },
          error: () => {
            this.productService.connectError();
          }
        }
      )
    )  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit(): void {
    // this.myInput.nativeElement.value='ahmedalaa';
    // console.log(this.catchProduct.products);
  }
  // onValueChange(catId:number){
  //   this.catId.next(catId);
  // }
  // priceChanged(event:number)
  // {
  //   this.TotalOrderprice=event;
  // }




// ////////////////test
// test1(x:number,y:(x:number)=>string)
// {
//   let z=y(5);
// }
// test()
// {
//   this.test1(5,x=>"hello:-"+x)
// }
// ///////////////////testt




// testt1(x:number,y:(this:IProduct,x:number)=>string)//this دي كلمة محجوزة هتبان الاستفادة منها في ال فانكشن فقط مش ف ال ارو فانكشن 
// {
//   let pro:IProduct={categoryId:4,id:5,name:"",pictureUrl:"",price:55,quantity:55,buyQuantity:33,category:""}
//     y.call(pro,44)
// }
// testt()
// {
//   // this.testt1(5,function xx(x)
//   // {
    
//   //     return ""+this.buyQuantity; // هتستفاد ب ال this فقط عن طريق الفانكشن العادية 
//   // }
  
//   // )
//     this.testt1(5,(x)=>{
//       // this. هنا ال this  خارجي 
//     return  ""
//     }
  
//   )
// }

}


/*
      [ngModel]="value | async"    لو القيمة اتغيرت ف ال TS هتتغير ف ال HTML وعملتها async لأان لأن:

value هو Observable، مش قيمة مباشرة
      
      
      (ngModelChange)="value.next($event)"   لو القيمة اتغيرت ف ال html هتتغير ف ال Ts

*/
