import { ActionCreatorProps, createAction, props } from "@ngrx/store";

//create action
export const langAction=createAction<string,{lang:string}>("[lang] changeLangyage",
//export declare function createAction<T extends string // T عبارة عن استرنج تايب, P extends object // بي عبارة عن اوبجيكت تايب >
// (type: T, config: ActionCreatorProps<P> & NotAllowedCheck<P>): ActionCreator<T, (props: P & NotAllowedCheck<P>) => P & Action<T>>;
props<{lang:string}>() // : ActionCreatorProps<P> // الميثود هترجع حاجة من نوع  ActionCreatorProps<{lang:string}> // زي كده //
// props<{lang:""}>() // لو عملت كده عادي لان "" subType of string // يعني عبارة عن تايب بردو والتايب ده سب تايب من استرنج
//number subType of unknown, "" subType of string, 5 subtype of number 
);














// //export interface ActionCreatorProps<T> { _as: 'props'; _p: T; } 
// export const x:ActionCreatorProps<{lang:string}>={
//     _as:'props',
//     _p:{lang:"en"}
// }

// export const TestlangAction=createAction<string,{lang:string}>("[lang] changeLangyage",x);










// //create actions

// import { ActionCreatorProps, createAction, props, RuntimeChecks } from "@ngrx/store";

// //export declare function createAction<T extends string, P extends object>(type: T, 
// // config: ActionCreatorProps<P> 
// // & NotAllowedCheck<P>): ActionCreator<T, (props: P & NotAllowedCheck<P>) => P & Action<T>>;

// //// config: ActionCreatorProps<P> 
// //export interface ActionCreatorProps<T> 
// // {
// //    _as: 'props'; // ده انترفيس بياخد اي دي وهي بتاخد القيمة دي فقط مش بتتغير 
// //    _p: T;        // دي بتاخد T
// // }
// // let cc:ActionCreatorProps<{lang:string}> ={_as:'props',_p:{lang:"en"}} // ده غلط

// // export declare function 
// // props<P extends SafeProps, SafeProps = NotAllowedInPropsCheck<P>>():
// //  ActionCreatorProps<P>; // هترجع حاجة م النوع ده : ActionCreatorProps<P>; 


// //T extends Primitive ? PrimitivesAreNotAllowedInProps : never;
// // props<boolean>();


// //T extends Primitive ? PrimitivesAreNotAllowedInProps : never;
// //لكن لو النوع مش primitive ومش object (وده نادر جدًا، زي void أو unknown) → يرجع never، ومعناه "مسموح".



//  //Gollllllllllllllllllllllllllllllllllllld
// // //////////////////Gold--تعليقاتك صح بنسبة 100٪.////////////////////////
// // class x{
// //     ptp<y>(){} // Y تشير الي اي تايب كان 
// //     pttp<y extends number>() {} // Y تشير الي حاجة من تايب نامبر
// //    ////////
// //     pp<x extends isallow,isallow=number>() // x تشير الي حاجة من تايب نامبر
// //     {
           
// //     }
// //       pop<x extends isallow,isallow=isAllowed<x>>()
// //     {
      
// //       // Gold   // كده التايب ده لازم يكون ب يعمل اكستينج ل isallow
// //                 //و ال isallow ده عبارة عن تايب isAllowed<x>
// //                 // ف هيدخل ف التايب ده export type isAllowed<o>= o extends number?  unknown:never;
// //                 // ومعاه ال o ولنفترض ان ال o دي هي type number مثلا 
// //                 // ف هترجع unknown 
// //                 // ف بكده هيتشك   number extends unknown  // لان ال x افترضناها ب number و isallow هتساوي unknown
// //                 // وده طبعا فاليد 
// //                 // بس كده 
// //     }

// //     cc()
// //     {
// //         this.pop<void>()  // غلط لان isAllowed<void> هتساوي never , وبالتالي x extends isallow هتبقي void extends never  ف انفاليد طبعا  
// //         this.pop<never>() // هنا هيوافق عليه لان ال never بتعمل اكستيند ل اي تايب 
// //         this.pop<number>() 
// //         this.pop<"unknown">() 
// //     }
// //     //////
// // }
// // export type isAllowed<o>= 
// //  o extends "unknown"? 
// //   "unknown":
// //   o extends number?unknown:never 

// //  ; 

// //  type xs=5; // كده طب تايب محدد يعني لو كده type xs=number هيبقي xs دي هتقبل اي رقم انما كده type xs=5; هتقبل 5 بس 
// //  // ف هنا الخمسة استخدمت ك نوع مش ك قيمة ب الدليل اني اقدر اعمل كده //let xs:number=5; let xss:5=5;
// // type z={name:"ahmed",nam:string}
// // interface Ix{
// //     name:5,
// //     ss:string
// // }
// // type zz=Ix;
// // let xxx:zz={name:5,ss:""};
// // let xxxx:Ix={name:5,ss:""};
// // //////////////////////////////////////////////////////

// export const changeLangAny = createAction(
//   '[Lang] Change Language Any',
//   props<{sL:6}>// بياخد جينيرك  تايب وال جينيرك تايب ده لازم يكون كده 
//   /*
//         أمثلة سريعة لتوضيح الناتج
//         T	                                     نتيجة NotAllowedInPropsCheck<T>
//         string	                                PrimitivesAreNotAllowedInProps
//         number[]    	                        ArraysAreNotAllowedInProps
//         { type: 'x'; name: string }	            TypePropertyIsNotAllowedInProps
//         {}	                                    EmptyObjectsAreNotAllowedInProps
//         { id: number }	                        unknown (مسموح)
       
//        P extends object لازم تكون اوبجيكت غير كده مش هيقبل  
//         void,...                                never
        
//   */
  
  
//   () //: ActionCreatorProps<P>  هترجع حاجة م النوع ده
// );

// /*

// import { createAction, props } from '@ngrx/store';

// // 1- النوع العام (lang: string)
// export const changeLangAny = createAction(
//   '[Lang] Change Language Any',
//   props<{ lang: string }>()
// );

// // 2- النوع المقيد بالقيمة "en"
// export const changeLangFixed = createAction(
//   '[Lang] Change Language Fixed',
//   props<{ lang: "en" }>()
// );

// // -------------------
// // تجربة الاستدعاء:

// // النوع العام: يقبل أي نص
// changeLangAny({ lang: "en" }); // ✅
// changeLangAny({ lang: "ar" }); // ✅
// changeLangAny({ lang: "fr" }); // ✅

// // النوع المقيد: يقبل "en" فقط
// changeLangFixed({ lang: "en" }); // ✅
// changeLangFixed({ lang: "ar" }); // ❌ Error: Type '"ar"' is not assignable to type '"en"'


// */