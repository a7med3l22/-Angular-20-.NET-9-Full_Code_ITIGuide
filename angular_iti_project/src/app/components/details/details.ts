import { Component, ElementRef, OnDestroy, OnInit, ViewChild, viewChild } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { StaticProducts } from '../service/static-products';
import { IProduct } from '../../models/iproduct';
import { ChangeColor } from '../../directives/change-color';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/Forms';
import { ProductDetails } from '../service/product-details';
import { filter, max, Subscription } from 'rxjs';
import { Product } from '../product/product';
import { ProductService } from '../service/product-service';

@Component({
  selector: 'app-details',
  imports: [CommonModule, FormsModule,ChangeColor], //import directive
  templateUrl: './details.html',
  styleUrl: './details.css'
})
export class Details implements OnInit,OnDestroy{
dateTime=Date.now();
id!:number;
product!:IProduct;
hiegstId:number=0;
lowerId:number=0;
products:IProduct[]=[];
isNextDisabled:boolean=false;
isPerviousDisabled:boolean=false;
currentindexId!:number;
productsLength!:number;
IsincluetotalOrderInUrl!:boolean;
subscribe=new Subscription;
getProductById!:IProduct;
// @ViewChild('nextButton') myNextButton!: ElementRef<HTMLButtonElement>;
// @ViewChild('perviousButton') myPerviousButton!: ElementRef<HTMLButtonElement>;

/**
 *
 */
constructor(private productService:ProductService,private _productDetails:ProductDetails,private _activatedRoute:ActivatedRoute,private _router:Router,private _staticProduct:StaticProducts,private _location:Location) {
  
  // const products = _staticProduct.products.map(prd => prd.id ?? 0);
  // if (products.length > 0) {
  //   this.hiegstId = Math.max(...products);
  //   this.lowerId = Math.min(...products);
  // } 


}
ngOnDestroy(): void {
  this.subscribe.unsubscribe();
}
ngOnInit(): void {
  // debugger;
  this.productsLength=this.products.length;
  
    this.subscribe.add(

this.productService.$updatedApiProductsITI.subscribe(
  // انا استدعيت الميثود subscribe() وبعتله فيها  value=>this.products=value  ف هو داخليا جوه داله ال سبسكرايب هيعمل انفوك لل بريديكيت اللي هو بيستقبلها وهيبعتلها نسخة من الفاليو اللي حصلها تغيير ولما يبعتلها داله هييجي هنا ينفذ اللي موجود وبس كده -- استدعيت الداله وبعتله بريديكيت فيها -- جوه الداله عمل انفوك لل بريديكيت وبعتله نسخة من الفاليو وبعدين جيه هنا نفذ اللي مطلوب وبعدين رجع ل جوه الفانكشن تاني ملهاش حاجة ف خرج 
  //جااااااااامد
  value=>this.products=value
)

    );


  this._activatedRoute.paramMap.subscribe(param => { // عامل سبسكرايب علشان كل م ال url يتغير ينفذ اللي جوه ال سبسكرايب
  //  debugger;
    const id = Number(param.get('id'));

    //get current index by current id

    this.currentindexId=this.products.findIndex(prd=>prd.id==id);
    if (isNaN(id)) {
        this._router.navigate(['notFound']);
    } else {
        this.id = id;
                // debugger;

        // علشان كل م يتغير ال اي دي ينفذ دي
        this.productService.getProductById(this.id).subscribe( // هيدخل بعد اللفه الاولي 
            value=>
              {
                // debugger;
                this.getProductById=value
              
                if(this.getProductById)
                {
                  // debugger;
                  this.product=this.getProductById;
                  
                }
                else{
                  this._router.navigate(['notFound'])
                }
  
              }
        );
          
    }
    // debugger;
    this.updateButtonColor(); 
    // أي حاجة تتحدف ال ts بيتم تحديث القيم اللي معروضة في ال  html تلقائي
/*
    لو ف نفس الكمبوننت details 
    وغيرت بس ال id 
    كده الراوتر مش هيحمل من اول وجديد 
    علشان كده عملت سبسكرايب
*/
});

}
// ngAfterViewInit()
// {
//   this.updateButtonColor();
// }
getProductDetails(productprice:number,productquantity:number,productId:number)
{
  this._productDetails.setProductValues(productprice,productquantity,productId)
}
back()
{
  //  this._router.navigate(['/totalOrder/order'])
  this._location.back();
}
next()
{
  // if(this.id<=this.hiegstId-1)
  // {
  //   this._router.navigate(['totalOrder/details',this.id+1]);
  
  // }
  // اول حاجة هجيب ال انديكس اللي هو واقف عنده بناء علي ال id 
  // debugger;
 var index=this.products.findIndex(prd=>prd.id==this.id);
 var lastindex=this.products.length-1;
if(index!=lastindex)
{
    var nextIndex=index+1;
    // عاوز اجيب ال اي دي بتاع ال نيكست اندكس
     var nextId=this.products.at(nextIndex)?.id;
     this._router.navigate(['/totalOrder/details',nextId]);
}
else{
  // لو وصل لل اخر عنصر عاوزة يروح ل اول عنصر  يبقي هجيب ال اي دي بتاع اول عنصر واعمل نافجيت ليه ولما اعمل نيكست هيجيب الاندكس الحالي من ال url وهيكمل بقي الدنيا 

    var firstItemId=this.products.at(0)?.id;
    this._router.navigate(['/totalOrder/details',firstItemId]);
    }
}

previous()
{
  // if(this.id>=this.lowerId+1)
  // {
  //   this._router.navigate(['totalOrder/details',this.id-1]);
  // }
 var index=this.products.findIndex(prd=>prd.id==this.id);
 var firstindex=0;
if(index!=firstindex)
{
  // عاوز اجيب ال لاست انديكس
   var lastIndex=index-1;
   var lastId=this.products.at(lastIndex)?.id;
   this._router.navigate(['/totalOrder/details', lastId]);
}
else{
    // عاوزة لما يوصل ل اول عنصر يروح ل اخر عنصر
    var lastItemId=this.products.at(this.products.length-1)?.id;
    this._router.navigate(['/totalOrder/details',lastItemId]);

}
}
private updateButtonColor() {
      // if (!this.myNextButton || !this.myPerviousButton) return; // حماية لان اول اما يدخل ليه من ngOnInit اول مرة هيبقي البوتونز ان ديفيند علشان كده استخدمت ngAfterViewInit 

     // var currentIndex=this._staticProduct.products.indexOf(this.product); // دي او اللي تحتها عادي
        //  debugger;
     var currentIndex=this.products.findIndex(prd=>prd.id==this.id)
          // if(currentIndex==this._staticProduct.products.length-1)
          // {
          //     this.myNextButton.nativeElement.style.background='red';
          //   this.myNextButton.nativeElement.disabled = true;

          // }
          // else if(currentIndex==0)
          // {
          //   debugger;
          //     this.myPerviousButton.nativeElement.style.background='red';
          //   this.myPerviousButton.nativeElement.disabled = true;

          // }
          // else{
          //     this.myNextButton.nativeElement.style.background='';
          //     this.myPerviousButton.nativeElement.style.background='';
          //     this.myNextButton.nativeElement.disabled = false;
          //     this.myPerviousButton.nativeElement.disabled = false;
          // }
  
          
          this.isNextDisabled=(currentIndex===this.products.length-1);
          this.isPerviousDisabled=(currentIndex===0);
}

// findhigestId_lowerId()
// {
//   //عاوز اجيب اقل id واكبر id ب انه يقارنهم ببعض 
//   var lastIndex=this._staticProduct.products.length-1;
//   var higestId=this._staticProduct.products.at(0)?.id!;
//   var lowerId=this._staticProduct.products.at(0)?.id!;
//   for(let i=0;i<lastIndex;i++)
//   {
//     //[4,6,12,3]
//       if(this._staticProduct.products.at(i+1)?.id!>higestId)
//       {
//           higestId=this._staticProduct.products.at(i+1)?.id!;
//       }
//       if(this._staticProduct.products.at(i+1)?.id!<lowerId)
//       {
//         lowerId=this._staticProduct.products.at(i+1)?.id!;
//       }
//   }
// // طريقة اخري  
// const ids = this._staticProduct.products.map(p => p.id ?? 0); // [اري من ال اي ديز ولو ملقاش ف برودكت اي دي هيحطه بصفر]

// const highestId = Math.max(...ids); //[ألاعلي ف ال اري دي ]
// const lowestId = Math.min(...ids);

// }
}
