import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductDetails {
  // productprice!:number;
  // productquantity!:number;
  // productId!:number;

  private _productprice=new BehaviorSubject<number>(0);
  private _productquantity=new BehaviorSubject<number>(0);
  private _productId=new BehaviorSubject<number>(0);

  $productprice=this._productprice.asObservable();
  $productquantity=this._productquantity.asObservable();
  $productId=this._productId.asObservable();

  setProductValues(productprice:number,productquantity:number,productId:number)
  {
    this._productprice.next(productprice);
    this._productquantity.next(productquantity);
    this._productId.next(productId); // اخر حاجة بيتعملها set علشان كده هسحب التوتال فيها 
  }

}
