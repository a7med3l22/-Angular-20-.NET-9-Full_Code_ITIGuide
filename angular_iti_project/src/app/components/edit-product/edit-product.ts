import { Component, ElementRef, OnInit, ViewChild, viewChild } from '@angular/core';
import { ActivatedRoute, NavigationCancel, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { ProductService } from '../service/product-service';
import { IProduct } from '../../models/iproduct';
import { ICategory } from '../../models/icategory';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/Forms';

@Component({
  selector: 'app-edit-product',
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-product.html',
  styleUrl: './edit-product.css'
})
export class EditProduct implements OnInit {
  @ViewChild('uploadedFile') fileInput!: ElementRef;
dateTime=Date.now();
  product!: IProduct;
  ExistImg!: string;
  //عاوزة ياخد ال id من ال اكتيف لينك 

  /**
   *
   */
  name!: string;
  categoryId!: number;
  categories!: ICategory[];
  price!: number;
  quantity!: number;
  pictureFile?: File | null;
  previewUrl: string | null = null;

  removable: boolean = false;
  constructor(private location:Location,private activeRoute: ActivatedRoute, private router: Router, private productService: ProductService) {


  }
  ngOnInit(): void {
    //Observable<ParamMap>;           مش محتاج اعمله اوبسيرفابول لاني لما اضغط هنا ال id مش هحتاح اني اغيره ف نفس الكمبوننت دي 
    // this.activeRoute.paramMap.subscribe(
    //      par=>
    //      {
    //       const ID=Number(par.get('id'));
    //         if(isNaN(ID))
    //         { 
    //           this.redirectToNotFound();
    //            return;
    //         }

    //         this.par=ID;
    //      } );
    // debugger;
    const ID = Number(this.activeRoute.snapshot.paramMap.get('id'));
    if (isNaN(ID)) {
      this.redirectToNotFound();
      return;
    }
    this.productService.getProductById(ID).subscribe(
      val => {
        if (!val) {
          this.redirectToNotFound();
          return;
        }

        this.product = val
        this.categoryId = val.categoryId;
        this.ExistImg = val.pictureUrl;
        this.name = this.product.name;
        this.quantity = this.product.quantity;
        this.price = this.product.price;
      }
    )
    this.productService.$updatedCategoryProductsITI.subscribe( // هيجيبلي اخر قيمة محفوظة فيه لانه BehaviorSubject ك قيمة ابتداشية ولو حصل اي تغيير تاني بعد م خد اخر قيمة فيه هييجي هنا طبعا 
      value =>
        this.categories = value
    )




  }
  submitForm() {
    // debugger;
    // عاوزة ياخد البيانات دي كلها ويبعتها بقي للباك اند 
      this.productService.updateProduct({
      categoryId: this.categoryId,
      name: this.name,
      price: this.price,
      ProductId: this.product.id,
      quantity: this.quantity,
      pictureFile: this.pictureFile
    }).subscribe(
      {
        next: (val) => {
          if (val) {
            this.product = val
            this.categoryId = val.categoryId;
            this.ExistImg = val.pictureUrl;
            this.name = this.product.name;
            this.quantity = this.product.quantity;
            this.price = this.product.price;
            this.fileInput.nativeElement.value = "";
            this.removable = false;
            // debugger;
              this.showAlerts();
            return;
          }
          // لو عملت ابديت ومحطتش صورة ومكانش موجود الصورة دي ف الداتا بيز هيرجعلك فولس 
          //	return Ok(false); //عاوزة يرجع فولس ف حالة ان الصورة القديمة مش موجوده ومش باعت صورة 
          Swal.fire({
            icon: 'warning',
            title: 'Must Add Image ',
            text: 'This Product Image Is Deleted From DataBase!!',
          });
        },
        error: (err) => {
          Swal.fire({
            icon: 'error',
            title: 'Dont Updated Product Successfully!!'
          });
          console.log(err);

        }
      }
    );
  }
  AddImage(event: Event) {
    // debugger;
    var fileEvent = event.target as HTMLInputElement;
    var file = fileEvent.files?.item(0);

    if (!file) {
      this.previewUrl = null;
      this.pictureFile = null;
      return;
    }
    if (!file?.type.startsWith('image/')) {
      Swal.fire({
        icon: 'error',
        title: 'File is not an image',
        text: 'Please upload a valid image file (jpg, png, etc).',
      });
      this.fileInput.nativeElement.value = "";
      return;
    }
    this.pictureFile = file;
    this.previewUrl = URL.createObjectURL(file);
    this.removable = true;
  }
  private redirectToNotFound(): void {
    this.router.navigate(['/notFound']).then(res => {
      if (res) {
        // not found ID! , will Redirect to notFound Page
        Swal.fire({
          position: "top-end",
          icon: "error",
          title:
            "not found ID! ,Page will Redirect to NotFound Page",
          showConfirmButton: false,
          timer: 2000
        });
      } else {
        //Error When Redirect To notFound component!!
        Swal.fire({
          icon: "error",
          title: "Oops...",
          text: "Something went wrong,Error When Redirect To notFound component!!!",
        });
      }

    }
    )

  }
  removeUploadedImage() {
    if (this.previewUrl) {
      URL.revokeObjectURL(this.previewUrl); // ⛔ يحرر المسار من الذاكرة
    }
    this.previewUrl = null;
    this.pictureFile = null;
    // const elem = document.getElementById("formFile");
    // elem?.addEventListener(   //<K extends keyof HTMLElementEventMap>(type: K, listener: (this: HTMLElement, ev: HTMLElementEventMap[K]) => any, options?: boolean | AddEventListenerOptions): void;
    //   "change",()=>{
    //     debugger;
    //         elem.textContent="dddd"       
    //   }
    // )

    this.fileInput.nativeElement.value = "";
    this.removable = false;

  }
  async showAlerts() {
  await Swal.fire({
    icon: 'success',
    title: 'Added Successfully',
    timer: 1500,
    showConfirmButton:false
  });
 // اويت علشان يستنوا بعض لانها عمليات متزامنة
  const result = await Swal.fire({
    title: "Do you want to Go To Orders?",
    showCancelButton: true,
    confirmButtonText: "Yes Go To Orders", // اختياري
    cancelButtonText:"No Stay In This Page" // اختياري
  });

  if (result.isConfirmed) {
    this.router.navigate(['/totalOrder']);
//  var x=new EditProduct(this.activeRoute,this.router,this.productService);
//      this.productService.x({edit:x,xxx:5})
  }
}
Back()
{
  this.location.back();
}
}
