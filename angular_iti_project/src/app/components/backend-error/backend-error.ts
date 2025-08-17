import { Component, Directive, OnInit } from '@angular/core';
import { ChangeColor } from '../../directives/change-color';
import { Router } from '@angular/router';
import { ProductService } from '../service/product-service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-backend-error',
  imports: [ChangeColor],
  templateUrl: './backend-error.html',
  styleUrl: './backend-error.css'
})
export class BackendError implements OnInit {



  // [Symbol.iterator]():Iterator<number> {
   
  // const arr = [10, 20, 30];

  // const iter = arr [Symbol.iterator]();
  //   return iter;
  // }
  /**
   *
   */
  constructor(private router:Router,private productService:ProductService) {
    
  }
  ngOnInit(): void {
    Swal.fire({
  title: "Are you sure?",
  text: "Error Wile Connecting To BackEnd!",
  icon: "warning",
  showCancelButton: true,
  confirmButtonColor: "#3085d6",
  cancelButtonColor: "#d33",
  confirmButtonText: "Yes, Reload!"
}).then((result) => {
  if (result.isConfirmed) {
  // debugger;
  this.router.navigate(['/totalOrder']).then(
    (bool)=>{
      if(!bool)
        {
        window.location.reload();
      }
    }
  );
  }
});

  }
Reload()
{
this.router.navigate(['/totalOrder']).then( // لو راح ورجع النتيجة ب فولس يعمل ريلود 
    (bool)=>{
      if(!bool)
        {
        window.location.reload();
      }
    }
  );
}
}
