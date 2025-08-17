//make effect on languagw

import { Injectable } from "@angular/core";
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { Action } from "@ngrx/store";
import { Observable } from "rxjs";
import { langAction } from "./language.actions";
import { tap } from 'rxjs';


@Injectable()
export class LanguageEffects
{
langEffect;
// test;
constructor(private  actions$ :Actions) {
// this.test=""
this.langEffect=createEffect(

    ()=>{
       return this. actions$ .pipe(
            ofType(langAction) // عاوز لانج اكشن // فلتر عليه
            ,
            tap(
                // (val)=>
                // {
                //    localStorage.setItem('',val.lang); 
                // } //Or جامد
                ({lang})=>
                {
                   localStorage.setItem('language',lang); 
                }
            )
       )
    }, 
  { dispatch: false }  // هنا محدد إن مفيش أكشن جديد هيطلع من الـ Effect

  

);

}
    
/*
    export declare function createEffect<Source extends () => Observable<Action>>
    (source: Source
         & ConditionallyDisallowActionCreator<true, ReturnType<Source>>, config: EffectConfig & {
        functional: true;
        dispatch?: true;
    }): FunctionalEffect<Source>;
*/



}

