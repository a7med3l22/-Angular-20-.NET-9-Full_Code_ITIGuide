//make lang interface ang intialize it
export interface IlanguageState
{
    lang:string;
}
export const LangStateValue:IlanguageState=
{
    lang: localStorage.getItem('language') ?? 'en' // لو مش موجوده خليها ب en 
}