import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IlanguageState } from "./language.state";

//select from store
 const selectLanguageStateInStore =createFeatureSelector<IlanguageState>('languageState') // بعرفه ان الكي من نوع  IlanguageState
//(s1: Selector<unknown, unknown>, projector: (s1: unknown) => unknown)
// export const selectCurrentLanguage=createSelector(selectLanguageStateInStore,s=>s.lang) // اختارها من الحاجات اللي ف ال LanguageStateInStore
export const selectCurrentLanguage=createSelector(selectLanguageStateInStore,({lang})=>lang) //or above جامد