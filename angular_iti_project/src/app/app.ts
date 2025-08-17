import { Component, EventEmitter, Input, NgModule, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { Navbar } from "./components/navbar/navbar";
import { Product } from "./components/product/product";
import { Footer } from "./components/footer/footer";
import { Order } from './components/order/order';
import { filter, Observable, Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ProductService } from './components/service/product-service';
import { HttpBackend, HttpClient } from '@angular/common/http';
import { Store } from '@ngrx/store';
import { selectCurrentLanguage } from './language/store/language.selectors';

@Component({
  selector: 'app-root',
  imports: [Navbar, Footer, RouterOutlet,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App  implements OnInit,OnDestroy{
      // isConnect:boolean=true;
subscribe=new Subscription;
   IsincluetotalOrderInUrl!:boolean;
  langStore$:Observable<string>
  constructor(private store:Store, private _router:Router,private productService:ProductService) {


    this.langStore$=this.store.select(selectCurrentLanguage);
  }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }
  ngOnInit(): void {

// this.subscribe.add(
//  this.productService.$isConnect.subscribe(
//       value=>this.isConnect=value
//     )
// )
   
// this.subscribe.add(

//     this.productService.tryConnect().subscribe(
//       value=>this.isConnect=value
//     ))
  }

  search:string='';
  protected title = 'angular_iti_project';
  
}
