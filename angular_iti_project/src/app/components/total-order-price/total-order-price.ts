import { Component, OnDestroy, OnInit } from '@angular/core';
import { Order }  from '../order/order';
import { ChangeColor } from '../../directives/change-color';
import { RouterOutlet } from '@angular/router';
import { ProductDetails } from '../service/product-details';
import { Subscriber, Subscription } from 'rxjs';
import { ProductService } from '../service/product-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-total-order-price',
  imports: [ChangeColor, RouterOutlet,CommonModule],
  templateUrl: './total-order-price.html',
  styleUrl: './total-order-price.css'
})
export class TotalOrderprice implements OnDestroy, OnInit {
  /**
   *
   */
  /**
   *
   */
 
  // private _price: number = 0;
  // private _quantity: number = 0;
  private _productId: number = 0;

  // set price(value: number) {
  //   this._price = value;
  // }
  // get price() {
  //   return this._price;
  // }
  // isConnect!:boolean;
  price: number = 0;
  quantity: number = 0; //عملتهم كده لاني مش محتاج استخدم لوجيك فيهم زي البرودكت اي لان اخر حاجة بتتعملها سبسكرايب هي البرودكت اي دي علشان كده عملت اللوجيك فيها علشان كله يبقي اخد القيم 
  // set quantity(value: number) {
  //   this._quantity = value;
  // }
  // get quantity() {
  //   return this._quantity;
  // }
  set productId(value: number) {
    
    this._productId = value;
    this.getTotal();
  }
  get productId() {
    return this._productId;
  }


  subscriptions = new Subscription();

  // productDetailsIdSC!:Subscription;
  // productDetailspriceSC!:Subscription;
  // productDetailsquantitySC!:Subscription;
  TotalOrderprice: number = 0;
  Dictionary: { [key: number]: number } = {};
  constructor(private productDetails: ProductDetails,private productService:ProductService) {

  }
  ngOnDestroy(): void {
    // debugger;
    this.subscriptions.unsubscribe();
    //  this.productDetailspriceSC.unsubscribe();
    //  this.productDetailsquantitySC.unsubscribe();

  }
  ngOnInit() {
    // debugger;
    /*  انا عملت كده    this.filterSubscription =   علشان امسك النسخة ف المتغير ده وبعدين اعمل دستروي للنسخه اللي انشأتها دي عن طريق المتغير ده لاني مسكتها من المتغير  ده وغير كده مكنتش هعرف امسكها واعمل للنسخه اللي انشأتها دي ديستروي */
    // ✅ الاشتراك لمتابعة التحديثات

// debugger;
    // this.subscriptions.add(
    // this.productService.$isConnect.subscribe(
    //   value=>
    //   {
    //     // debugger; //Default Value Is True 
    //     this.isConnect=value;
    //   }
    // ));
    this.subscriptions.add(
      this.productDetails.$productId.subscribe(productId => {
        this.productId = productId;
      }));
    this.subscriptions.add(
      this.productDetails.$productprice.subscribe(productprice => {
        // debugger;
        this.price = productprice;
      }));
    this.subscriptions.add(
      this.productDetails.$productquantity.subscribe(productquantity => {
        this.quantity = productquantity;
      }));

    //   this.productDetailsIdSC =this.productDetails.$productId.subscribe(productId => { 
    //   this.productId = productId;
    // });
    //   this.productDetailspriceSC =this.productDetails.$productprice.subscribe(productprice => { 
    //     // debugger;
    //   this.price = productprice;
    // });
    //   this.productDetailsquantitySC =this.productDetails.$productquantity.subscribe(productquantity => { 
    //   this.quantity = productquantity;
    // });
  }

  getTotal() {
    //if i choose same product, i will remove old product price=(oldQuentity*price) and will add new product price =(quentity*price) //By Me 3>
    // برافو عليك م شاء الله
    if (!(this.productId in this.Dictionary)) {
      // المنتج جديد
      this.TotalOrderprice += this.quantity * this.price;
      this.Dictionary[this.productId] = this.quantity;
    } else {
      // المنتج موجود
      this.TotalOrderprice -= this.Dictionary[this.productId] * this.price;
      this.TotalOrderprice += this.quantity * this.price;
      this.Dictionary[this.productId] = this.quantity;
    }



  }



}
/*
✅ الـ Function دي بترجع "حاجة من نوع" HttpFeature<HttpFeatureKind.Fetch>
يعني:
declare function withFetch(): HttpFeature<HttpFeatureKind.Fetch>;
المعنى هنا:

الدالة withFetch بترجع قيمة (value) أو كائن (object) من النوع HttpFeature<HttpFeatureKind.Fetch>

مش بترجع الكلاس نفسه (يعني مش بترجع blueprint أو constructor للكلاس)

📌 توضيح الفرق:
✅ 1. بترجع "حاجة من النوع":
ts
نسخ
تحرير
function createUser(): User {
  return new User(); // ده object من النوع User
}
❌ 2. لو كانت بترجع الكلاس نفسه:
ts
نسخ
تحرير
function getUserClass(): typeof User {
  return User; // الكلاس نفسه مش instance
}
✅ وبالتالي:
في حالتك:

ts
نسخ
تحرير
declare function withFetch(): HttpFeature<HttpFeatureKind.Fetch>;
يبقى الدالة بترجع object (instance) من النوع HttpFeature<HttpFeatureKind.Fetch>
وليس الكلاس أو الـ Type نفسه.
*/