import { Injectable, Input, OnDestroy, SimpleChanges } from '@angular/core';
import { IProduct } from '../../models/iproduct';
import { Product } from '../product/product';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { ProductService } from './product-service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StaticProducts implements OnDestroy {

  private updatedProduct=new BehaviorSubject<IProduct[]>([]);
  $products=this.updatedProduct.asObservable();
  //  products:IProduct[]=[]; // عاوز لما القيمة هنا تتغير الباقي اللي واخد القيمة منه يحث ويبعتله القيمة بعد التغيير يبقي اخليها اوبسرفابول
  //  filterProduct:IProduct[]=[];
    
  // ✅ إضافة BehaviorSubject
  private _filterProduct = new BehaviorSubject<IProduct[]>([]); 
  filterProduct$ = this._filterProduct.asObservable(); // يتم الاشتراك عليه علشان اعرف اعمله subscribe..

  subscribe=new Subscription;

   constructor(private productService:ProductService) {
// debugger;//Iam Sorry But 

      this.subscribe.add
      (
        
        this.productService.GetITIProducts()// هي لما دخلت هنا عملت فاير ولما عملت فاير مكانتش تعرف ان this.products بيعمل سبسكرايب عليها لانه لسه مكانش وصل لل .subscribe( دي , ف دي المشكلة 
      //   .subscribe(
      //     {
      //      next: value=>{
      //     // debugger; ////////Finally
      //     this.updatedProduct.next(value);
      //   },
      //   error:()=>
      //   {
      //      this.productService.connectError();
      //   }
      //     }
       
      //   // حل المشكلة انه يعرف ان this.products بتعمل سبسكرايب قبل م يحصل فاير 
      //   // وده م هيحصل غير لما  يعمل فاير وهو عارفه انمعمول سبسكرايب عليه 
      // )
     ); // كده هو عرف ان اتعمل سبسكرايب عليه عاوز افايره تاني بقي ف علشان افايره تاني اناديه تاني 
                //  debugger; //-- اول م دخل هنا عرف خلاص اني عامل سبسكرايب علي GetITIProducts //Hellooooooo 

    // this.productService.GetITIProducts(); // كده لما يدخل جواها هيحصل فاير ولما يحصل فاير هيكون عارف انه فيه حد عامل سبسكرايب عليه
   
      // this.products=[
      //   // عاوز اقارن اول عنصر بتاني عنصر واخد اكبر اي دي فيهم واخزنه ف هايست اي دي 
      //   //وبعدين اقارن ثالث عنصر ب هايست اي دي واكبر حاجة فيهم احطها ف هايست اي دي 
      //   {id:1,categoryId:1,name:"Laptop Dell Note 12",pictureUrl:"https://picsum.photos/id/42/200/300",price:50000,quantity:4,category:"test"},
      //   {id:2,categoryId:1,name:"Laptop  HP Zbook 15",pictureUrl:"https://picsum.photos/id/30/200/300",price:40000,quantity:3},
      //   {id:3,categoryId:2,name:"IPad Version 6",pictureUrl:"https://picsum.photos/id/50/200/300",price:30000,quantity:5},
      //   {id:4,categoryId:3,name:"Mouses toshiba",pictureUrl:"https://picsum.photos/id/80/200/300",price:10000,quantity:8},
      //   {id:5,categoryId:4,name:"Papers samsung",pictureUrl:"https://picsum.photos/id/140/200/300",price:570000,quantity:0},        
      //   {id:6,categoryId:5,name:"Printers apple",pictureUrl:"https://picsum.photos/id/20/200/300",price:80000,quantity:2},
      //   {id:7,categoryId:5,name:"Printers corn",pictureUrl:"https://picsum.photos/id/60/200/300",price:60000,quantity:6}
      //   // عاوز لما ابقي واقف عند ال 5 واعمل نيكست يجيبلي 2 ونيكست 1 ولست 2 وهكذا (Done) اقصد لو ال اي دي مش مترتبة هبدأ اجيب المنتجات ع حسب ال انديكس زس مس عملت
      // ]
    }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }

    // getAllProducts():IProduct[]
    // {
    //   return this.products; // هيعمل ريتيرن ف اول لفة ولما قيمة ال this.products تتحدث بعد اول لفة مش هيدخل هنا تاني لانه بيدخل بعد اول لفة بس  ف انا عاوز ميعملش ريتيرن هنا عاوزة ياخد القيمة من فوق علشان لما تتحدث يشوف التحديث 
    // }
    // filterProducts(receiveCategoryId?:number)
    // {
    //   // this.value!=undefined?this.filterProduct=this.products.filter(product=>product.id==this.value):this.products;
    //     //@if((value == undefined || product.categoryId == value) && product.quantity>0)
    
    //     // debugger;
    //     this._filterProduct.next(this.updatedProduct.value.filter(product=>((receiveCategoryId==undefined||product.categoryId==receiveCategoryId)&&product.quantity>0)));
    // }
    // search(value:string)
    // {
    //     this._filterProduct.next(this.updatedProduct.value.filter(p=>p.name.toLowerCase().includes(value.toLowerCase())));
    // }
    // getProductById(id:number):IProduct|null
    // { 
    //     let product= this.updatedProduct.value.find(prd=>prd.id==id);
    //    return product?product:null;
    // }
    
}
