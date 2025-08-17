import { Component, EventEmitter, Input, input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { IProduct } from '../../models/iproduct';
import { ICategory } from '../../models/icategory';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/Forms';
import { ChangeColor } from '../../directives/change-color';
import { PowerPipe } from '../../pipes/power-pipe';
import { StaticProducts } from '../service/static-products';
import { BehaviorSubject, Subscriber, Subscription } from 'rxjs';
import { Search } from '../service/search';
import { Route, Router } from '@angular/router';
import { ProductDetails } from '../service/product-details';
import { ProductService } from '../service/product-service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-product',
  imports: [CommonModule, FormsModule, ChangeColor],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product  {
  // عاوز ابعت ال Cat Id الحديث لل برودكت سيرفس 
    dateTime=Date.now();
    num:number=5;
    MyfilterProduct!:IProduct[];
    ApiITIProducts!:IProduct[];
    // هخلي ال برودكتش اوبسيرفابول بحيث لما يتغير الباقي يعرف 
    // private updatedPtoducts=new BehaviorSubject<IProduct[]>([]);
    // $products=this.updatedPtoducts.asObservable();
    // products!:IProduct[]; ///////// كده اول م يجيب الداتا من ال API هتتحط هنا فورا 
    // @Input() search:string='';
    filterProduct!:IProduct[];// عاوز ميعملش فيلتر الا اول م يجيب الداتا من ال API 
    Subscription=new Subscription(

      // ()=>{
      //   // دي لو عاوز اعمل حاجة اول م الكونستراكتور يعمل unsubscribe
      //   console.log("Cleanup logic"); //sub.unsubscribe(); // هيطبع "Cleanup logic"
      //     this.isUnsubscriped=true
      // }
    ); // للاشتراك
    categories:ICategory[]=[];
    TotalOrderprice:number=0;
    Dictionary: { [key: number]: number } = {};
    newlyAddedProduct?: IProduct; // my be undefined
    color:string='';
    @Output() onChangeprice:EventEmitter<number> = new EventEmitter<number>();
    receiveCategoryId:number|null=null;  // عاوز القيمة الابتدائية ليه ياخدها من ال سبسكرايب 
    // بيتتحط ف ال اون اتشينج لو حطيت قيمة من الابن عن طريق الاب فقط يعني [receiveCategoryId]=قيمة موجوده ف الاب 
    
    
    
    
    /*
       dictionary={{5,100},{6,700}}     
     */

// searchSC: string = '';



  // set searchSC(value: string) {
  //   this._staticProducts.search(value); // ✅ [جااااااااااااااامد <3]
  // }



    constructor(private product:ProductService,private productService:ProductService,private _staticProducts:StaticProducts,private _search:Search,private router:Router,private productDetails:ProductDetails) {
// debugger;




      }
      ngOnInit() {


this.productService.$updatedApiProductsITI.subscribe(

  value=>
  
    {
    //  debugger;//////// هيدخل ولا لا 

      this.ApiITIProducts=value;
      this.MyfilterProduct=value;

    }
    
)

this.Subscription.add(
      this.productService.$search.subscribe(
        value=>
          {
          // debugger;
          // debugger;
        this.MyfilterProduct = this.ApiITIProducts.filter(p =>
          p.name.toLowerCase().includes(value?.toLowerCase() || '')
        );
        }
      ));

// عاوز ال كاتيجوري اي دي 
this.Subscription.add(
      this.productService.$catId.subscribe(

        value=>
        {
          // debugger;
                this.receiveCategoryId = value; // عاوز لما ال برودكتس تتغير ال فيلتر يتغير هو كمان 

            // debugger;
            console.log(this.ApiITIProducts);
         this.MyfilterProduct= this.ApiITIProducts.filter(p=>
        {
        //  لو عملت اقواس لازم اعمل ريتيرن !!
         return ((value==null||p.categoryId==value)&&p.quantity>0)
         // لو معملتش ريتيرن كده لما يتعملها انفوك الفانكشن من جوه مش هتستقبل حاجة ف ب التالي مش هيرجعلك حاجة ! 

        }
        
        );
        }
      ));

 this.Subscription.add(
  this.productService.GetITICategories().subscribe(
         {
           next: value=>{
          // debugger; ////////Finally
          this.categories=value;
        },
        error:()=>
        {
 this.productService.connectError();        }
          }
      )
      )

//   this.Subscription.add( 
//   this._staticProducts.$products.subscribe(
//     value=>this.updatedPtoducts.next(value)
//   )); 
//    this.Subscription.add( 
//       this.$products.subscribe(value=>
//         this.filterProduct=value
//   )); 

// // عاوزة ييجي هنا لما يجيب من الداتا بيز يعمل ابديت للقيم 
// this.productService.$updatedApiProductsITI.subscribe(

//   value=>this.updatedPtoducts.next(value)
// );




    //      this.Subscription.add(
    // this.productService.$catId.subscribe(catId => { 
    //   debugger;
    //   this.receiveCategoryId = catId; // عاوز لما ال برودكتس تتغير ال فيلتر يتغير هو كمان 
    // }));

              /*  انا عملت كده    this.filterSubscription =   علشان امسك النسخة ف المتغير ده وبعدين اعمل دستروي للنسخه اللي انشأتها دي عن طريق المتغير ده لاني مسكتها من المتغير  ده وغير كده مكنتش هعرف امسكها واعمل للنسخه اللي انشأتها دي ديستروي */
         // ✅ الاشتراك لمتابعة التحديثات
        //  debugger;
    //   this.Subscription.add(
    // this._staticProducts.filterProduct$.subscribe(products => { 
    //   this.filterProduct = products; // عاوز لما ال برودكتس تتغير ال فيلتر يتغير هو كمان 
    // }));
      
      
      
  // this.Subscription.add(
  //   this._search.$search.subscribe(str => {
  //   this.searchSC = str; // ✅ مجرد أن تتغير هنا، الـ Setter يشتغل تلقائيًا ويرسل القيمة للسيرفس
  // }));


}


edit(productId:number)
{
    this.router.navigate(['/Edit',productId])
}
  // ngOnChanges(changes: SimpleChanges){
  //   // debugger;
  //   if(changes['receiveCategoryId'])
  //   {
  //      this._staticProducts.filterProducts(this.receiveCategoryId);
  //   }
  //   if(changes['search'])
  //   {
  //      this._staticProducts.search(this.search);
  //   }
  // }
    ngOnDestroy() {
    // ✅ إلغاء الاشتراك لمنع الـ Memory leaks
    // debugger;
    this.Subscription.unsubscribe();

  }
  /*
  🧠 القاعدة الذهبية في JavaScript/TypeScript للـ arrow functions:
✅ لو استخدمت أقواس {} مع =>:
لازم تكتب return صراحةً علشان ترجّع القيمة.

ts
نسخ
تحرير
const sum = (a, b) => {
  return a + b; // ✅ لازم return هنا
};
✅ لو ما استخدمتش أقواس {} (يعني كتبتها في سطر واحد بدون بلوك):
الـ arrow function بترجع القيمة تلقائيًا (implicit return) من غير ما تكتب return.

ts
نسخ
تحرير
const sum = (a, b) => a + b; // ✅ ما فيش return، لأنها تلقائية
🧨 لو كتبت {} وما كتبتش return:
وقعت في الفخ 😅
JavaScript هتفتكر إنك بتكتب بلوك كود فقط، مش إنك بترجّع قيمة.

ts
نسخ
تحرير
const sum = (a, b) => {
  a + b; // ❌ غلط! مش هترجع حاجة
};
  
  */
  // getTotal(quantity: number, price: number,productId:number) 
  //   { 
  //     //if i choose same product, i will remove old product price=(oldQuentity*price) and will add new product price =(quentity*price) //By Me 3>
  //     // برافو عليك م شاء الله

  //     this.TotalOrderprice+=quantity*price; 
  //     if(productId in this.Dictionary==false) // بيتحقق من وجود قيمة 
  //       { 
  //         this.Dictionary[productId]=quantity;
  //       }
  //       else if(productId in this.Dictionary==true)
  //       {
  //         this.TotalOrderprice-=this.Dictionary[productId]*price;
  //         this.Dictionary[productId]=quantity;
  //       }

  //     this.onChangeprice.emit(this.TotalOrderprice); //(onChangeprice) هيفاير كل م ييجي هنا  // يعني هيروح يدور ع دي وينفذها 

  //   }
// setValue(value?:number)
// {
//   this.receiveCategoryId=value;
// }
// trackByFn(index:number, product:IProduct)
// {
//   return product.id;
// }
// addProduct()
// {

//     const newProduct = { id:8,categoryId:1,name:"Laptop",pictureUrl:"https://picsum.photos/id/92/200/300",price:50000,quantity:4 };
//     this.updatedPtoducts.value.splice(2,0,newProduct);
//     this.newlyAddedProduct=newProduct;
// }
// deleteAddedProduct(product?:IProduct)
// {
//   if (product) 
//     {
//       this.updatedPtoducts.next(this.updatedPtoducts.value.filter(p=>p.id!==product?.id)) //🔹 filter تنشئ مصفوفة جديدة تحتوي على جميع العناصر التي تحقق الشرط فقط.
//       this.newlyAddedProduct = undefined; // لإلغاء الإمساك بعد الحذف
//     // 🔹 filter تنشئ مصفوفة جديدة تحتوي على كل المنتجات ما عدا المنتج الذي له نفس id الممرر.
//     }
// }
changeColor(color:string)
{
  this.color=color;
}

route(product_id:number)
{
  this.router.navigate(['/totalOrder/details', product_id]);
}
getTotal(buyQuantity:number,price:number,id:number)
{
  // debugger;
  this.productDetails.setProductValues(price,buyQuantity,id);
}
Delete(product_id:number)
{
  Swal.fire({
  title: "Are you sure?",
  text: "You won't be able to revert this!",
  icon: "warning",
  showCancelButton: true,
  confirmButtonColor: "#3085d6",
  cancelButtonColor: "#d33",
  confirmButtonText: "Yes, delete it!"
}).then((result) => {
  if (result.isConfirmed) {

                  /////////
this.product.DeleteProduct(product_id);



                  //////




    Swal.fire({
      title: "Deleted!",
      text: "Your file has been deleted.",
      icon: "success"
    });
  }
});

  //  this.productService.GetCategoriesInProducts()
  //          .subscribe(
  //           ()=>  this.router.navigate(['/totalOrder'])
  //          );
}
}
