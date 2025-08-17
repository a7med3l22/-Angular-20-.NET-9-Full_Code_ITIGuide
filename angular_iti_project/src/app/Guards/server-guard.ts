import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ProductService } from '../components/service/product-service';
import { map, Observable, tap } from 'rxjs';

export const serverGuard: CanActivateFn = (route, state) => {
// debugger;  
  var productService=inject(ProductService)
  var _router=inject(Router);


  const Token = localStorage.getItem('AngularToken')||sessionStorage.getItem('AngularToken');

      if(!Token)
      {
          _router.navigate(['/login']);
        return false;
      }

      return productService.tryConnect().pipe( // كده كده هو هيعملها سبسكرايب لانه عامل حسابه انها ممكن ترجع حاجة اسينك 
      map(isConnect => {
        if (!isConnect) {
          // debugger;
          _router.navigate(['/ErrorFromBackEnd']);
          return false;
        }
        return true;
      })
    );

};
