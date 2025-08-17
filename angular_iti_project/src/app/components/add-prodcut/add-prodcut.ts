import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/Forms';
import { ProductService } from '../service/product-service';
import { ICategory } from '../../models/icategory';
import { Subscription } from 'rxjs';
import { IAddProduct, IProduct } from '../../models/iproduct';
import { Router } from '@angular/router';
import Swal from 'sweetalert2'
@Component({
  selector: 'app-add-prodcut',
  imports: [CommonModule,FormsModule],
  templateUrl: './add-prodcut.html',
  styleUrl: './add-prodcut.css'
})
export class AddProdcut implements OnInit,OnDestroy {

  // Product:IAddProduct={categoryId:5} as IAddProduct ;// لو عاوز اول عنصر يظهر خمسه // {} as IAddProduct	كائن فاضي بس افترضنا إنه من نوع IAddProduct (بس القيم undefined) // كده انا عملته كائن فاضي واديت تقيمة ل كاتيجوري اي دي ب صفر علشان محتاج اديله قيمة ابتدائية ب صفر علشان يعرضه ف السيليكت او خلاص هخليه ب ان ديفيند وخلاص
  Product:IAddProduct={} as IAddProduct ;// لو عاوز اول عنصر يظهر خمسه // {} as IAddProduct	كائن فاضي بس افترضنا إنه من نوع IAddProduct (بس القيم undefined) // كده انا عملته كائن فاضي واديت تقيمة ل كاتيجوري اي دي ب صفر علشان محتاج اديله قيمة ابتدائية ب صفر علشان يعرضه ف السيليكت او خلاص هخليه ب ان ديفيند وخلاص
      // Product:IAddProduct={
      // categoryId: null as unknown as number , // اكني بقوله اعتبرها نال بس صدقني هبعتلك رقم بعدين ف مشيها
      // name:null as unknown as string  ,
      // pictureUrl:null as unknown as File ,
      // price:null as unknown as number ,
      // quantity:null as unknown as number
      //  }
  // اديها قيم ابتدائية افضل   
  /*
  ✅ الخلاصة:
❌ {} as File = خطر، كأنك بتلبس كائن أي لبس وهو مش مؤهل.

✅ null as unknown as File = آمن، ومقصود تقول للمترجم: "هحدد القيمة لاحقًا، بس متزعليش دلوقتي".    pictureUrl:null as unknown as File
  
  */ 
  Categories!:ICategory[];
  subscribe=new Subscription;
  constructor(private productService:ProductService,private route:Router) {
    
  }
  ngOnDestroy(): void {
    this.subscribe.unsubscribe();
  }
  ngOnInit(): void {
    // debugger;
    // this.Product.pictureUrl.name;
    // this.Product = {} as IAddProduct; // فرضت نوع IAddProduct على كائن فاضي
    // this.Product.name; // TypeScript يسمح بالوصول للخاصية لأن النوع IAddProduct، لكن القيمة هتكون undefined فعليًا
    // this.Product.name = "ahmed"; // بضيف خاصية name فعليًا للكائن Product مع القيمة "ahmed"

this.subscribe.add(  this.productService.GetITICategories().subscribe(
    value=>this.Categories=value
  ));
  }
AddProduct()
{
 


    this.productService.AddProduct(this.Product);
    this.route.navigate(['/totalOrder']);


      
}
onFileSelected(event:Event)
{
  const target=event.target as HTMLInputElement;
  if(target.files&&target.files.length>0)
  {
     const file = target.files[0];
  
    if (!file.type.startsWith('image/')) {//✅ أنواع الصور اللي بيقبلها file.type.startsWith('image/'):image/pngإلخ...
      Swal.fire({
        icon: 'error',
        title: 'File is not an image',
        text: 'Please upload a valid image file (jpg, png, etc).',
      });
      return;
    }
    this.Product.pictureFile=file;
  }
  else{
      Swal.fire({
          icon: "warning",
          title: "Warning...",
          text: "Must Choose Another Photo!",
        });
  }
  /*
      ❌ ليه مينفعش [(ngModel)] مع type="file"؟
      لأن [(ngModel)] بيشتغل مع قيم زي string, number, boolean...
      لكن File جاي من event.target.files، ومش بينزل كـ "قيمة bindable"، فـ لازم تتعامل معاه يدويًا.
  */
}


}
/*

3. أنسب في تصميم API وإرسال البيانات
مثلاً في DTO:

ts
نسخ
تحرير
{
  name: "ahmed",
  pictureUrl: null
}
ده معناه: "مفيش صورة"،
لكن:

ts
نسخ
تحرير
{
  name: "ahmed"
}
ده ممكن يعني: "أنا نسيت أبعته أصلاً"، أو "ماهوش مطلوب".
*/

/*
📦 مثال حقيقي من الواقع:
لو عندك تحديث منتج:

ts
نسخ
تحرير
PATCH /products/5
{
  name: "New Name",
  pictureUrl: null
}
ده معناه:

"غيّر الاسم، وامسح الصورة."

لكن:

ts
نسخ
تحرير
PATCH /products/5
{
  name: "New Name"
}
ده معناه:

"غيّر الاسم، بس سيب الصورة زي ما هي."


*/