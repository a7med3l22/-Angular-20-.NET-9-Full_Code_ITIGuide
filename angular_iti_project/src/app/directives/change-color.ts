import { Directive, ElementRef, HostListener, Input, input, OnChanges, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[appChangeColor]'
})

export class ChangeColor implements OnChanges { //✅ نعم، سيعمل بشكل طبيعي، لأن Angular فقط يبحث عن الدالة ngOnChanges لتنفذ عند تغير @Input.

@Input() externalColor!:string; //<div externalColor> this.externalColor === '' وليست undefined أو null.
// @Input('appChangeColor') value: string[]|undefined;  //<div appChangeColor class="card-body d-flex flex-column"> اكني عملت كده <div appChangeColor="" class="card-body d-flex flex-column"> عمرها م تبقي ب نل او ان ديفايند
@Input('appChangeColor') value?: string[];// زي اللي فوقيها   //<div appChangeColor class="card-body d-flex flex-column"> اكني عملت كده <div appChangeColor="" class="card-body d-flex flex-column"> عمرها م تبقي ب نل او ان ديفايند
// @Input() triggerColorChange!: string; // ✨ إضافة متغير لتغيير اللون ديناميكيًا
// @Input() changeColor!:string; 

@Input() changeprice!:number; // هيتم استدعاءه ف ال اون اتشينجز فقط لو استخدمته ف الاب 

constructor(private ele:ElementRef){ //Angular صممت الـ Attribute Directives خصيصًا للتلاعب بالـ DOM

  }

  // ngOnChanges(change:SimpleChanges) // بتتنادي لو حصل تغيير ف الانبوت من بره 
  // {
  //   debugger;
  //   if(change['changeColor']&&this.changeColor) //✅ لو حصل تغيير في قيمة triggerColorChange و القيمة الجديدة مش فاضية (undefined أو null أو '')، نفذ اللي جوا.
  //   {
  //     this.ele.nativeElement.style.backgroundColor=this.changeColor;
  //   }
  //   else
  //   {
  //     this.ele.nativeElement.style.backgroundColor=this.value[0]??'gray';
  //   }

  // }


  /*
  🟩 خلاصة مُركزة:
✅ ngOnChanges يُنادَى عند:

استقبال قيمة مختلفة من الأب عن آخر قيمة تم استقبالها من الأب.

❌ لا يُنادَى عند:

إرسال الأب نفس القيمة السابقة دون تغيير.

حتى لو غير الابن القيمة داخليًا، لا يؤثر ذلك على ngOnChanges لأنه يتعامل فقط مع تغييرات القيم المستلمة من الأب.
  
  */
//✨ “لو firstChange = true، فـ previousValue = undefined دائمًا لأنها أول مرة يتم فيها تمرير القيمة عبر @Input.”
ngOnChanges(change:SimpleChanges)  // لو اتغير اي حاجة ف ال انبوت @input هييجي هنا 
{
  // debugger;
  if(!this.value)
  {
    this.value=[]
  }
  // debugger;
  if(change['changeprice']&&this.changeprice!=0)//this.changeprice!=0 ممكن اعمله كده this.changeprice  هتدي ترو لو القيمة مش ب صفر بردو
  {
    // debugger;
     this.ele.nativeElement.style.backgroundColor='green';
  }
      else
    {
      this.ele.nativeElement.style.backgroundColor=this.value[0]??'yellow';
    }
}

@HostListener('mouseenter')
onMouseEnter() {
  if(!this.value)
  {
    this.value=[]
  }
  this.ele.nativeElement.style.backgroundColor = this.value[1]??'red'; //this.value[2] === undefined
}

@HostListener('mouseleave')
onMouseLeave() {
  this.ele.nativeElement.style.backgroundColor = this.externalColor===''?'aqua': this.externalColor;
}
////// عاوز لما اغير السعر الباك جراوند يتغير ! 

}

/*
  constructor(private ele:ElementRef){}
اكني عملت كده 
 ele:ElementRef
 constructor(ele:ElementRef){
  this.ele=ele;
  }


*/

/*
في Angular:

🔹 لماذا لا تقرأ قيمة @Input الممررة من الخارج داخل constructor؟

لأن Angular:

ينشئ الـ Directive أو الـ Component أولاً وينفذ الـ constructor.

بعد الإنشاء، يقوم بربط (Binding) قيم الـ Inputs المرسلة من الخارج.

لذلك:

داخل constructor → القيمة تكون القيمة الافتراضية ('default') التي كتبتها.

بعد ذلك يتم تحديث القيمة الممررة من الخارج على الـ @Input.

   [ملحوظة بعد م الكونستراكتور يتنادي بيبتدي ياخد قيم ال @input ويحطها ف البروبيرتي][استخدم ngOnInit للوصول للقيم بعد الاستقبال.] *الخلاصة* 
-Continue=>-
يعني ngOnInit ده بيتنادي بعد الكوستراكتور ؟
قال ChatGPT:
نعم ✅ بالضبط،
ngOnInit يُستدعى بعد constructor.

إليك ترتيب التنفيذ في دورة حياة Angular بوضوح:

1️⃣ constructor()
🔹 يُنفذ عند إنشاء الـ Component أو الـ Directive.
🔹 هنا يمكنك:

حقن الـ services.

عمل initializations الداخلية التي لا تعتمد على القيم القادمة من @Input.
🔸 لا تكون قيم @Input قد وصلت بعد.

2️⃣ Angular يقوم بعمل Binding لقيم @Input القادمة من الخارج.

3️⃣ ngOnInit()
🔹 يُنفذ بعد تمرير قيم @Input من الخارج.
🔹 هنا يمكنك:

قراءة القيم الممررة من الخارج عبر @Input.

تنفيذ الأكواد التي تعتمد على هذه القيم.
*/