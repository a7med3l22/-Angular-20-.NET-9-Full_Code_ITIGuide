import { Action, ActionReducerMap } from "@ngrx/store";
import { IAppState } from "./app.state";
import { langReducer } from "../language/store/language.reducer";

export const AppReducer:ActionReducerMap<IAppState>=
{
    //export type ActionReducerMap<T, V extends Action = Action> = { [p in keyof T]: ActionReducer<T[p], V>; };
    languageState:langReducer //ActionReducer<IlanguageState, Action<string>>
}