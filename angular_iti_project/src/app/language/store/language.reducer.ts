import { ActionCreator, createReducer, on, ReducerTypes } from "@ngrx/store";
import { IlanguageState, LangStateValue } from "./language.state";
import { langAction } from "./language.actions";

// const x:(xx:IlanguageState,yy:string)=>string=(_,k)=>k;// tesssssssst
// const x:(xx:IlanguageState,yy:string)=>string=({lang},k)=>k;// tesssssssst
// const x:(xx:IlanguageState,yy:string)=>string=(cc,k)=>k;// tesssssssst
// const xx:(xx:number,yy:string)=>string // تايب
// = x;// tesssssssst


// function x(xx:number,yy:string) // لإاليو 
// {
//     return "5";
// }
export const langReducer=createReducer(
    LangStateValue,
    
    //...ons: ReducerTypes<IlanguageState, readonly ActionCreator[]>[]
    //(state: unknown extends State ? InferredState : State, action: ActionType<Creators[number]>): ResultState; // كده الانتلافيس بقي عبارة عن فانكشن
    on(langAction,
        
        //action=> لو مش عاوز ابعت ف البراميتر ال action كله ممكن اختار منه ال انا عاوزة كده {}
        //(state,action)=>(state,{lang})
            (state,{lang})=>
            {
                //{} ده كائن جديد (object literal) فارغ. //return { };يعني بتعمل كائن جديد فاضي .
                //return { ...state };	كائن جديد بنفس الخصائص


                    return {...state,lang:lang};
            }
        
    )
    
);


















// // كل حاجة ي تايب ي قيمة من نوع تايب 


// // export declare function 
// // createReducer<S, A extends Action = Action, R extends ActionReducer<S, A> = ActionReducer<S, A>>
// // (initialState: S,
// //  ...ons: ReducerTypes<S, readonly ActionCreator[]>[]
// //)
// // : R;

// export const langReducer=createReducer(
//     //initialState: S
//     // بتاخد اي قيمة من نوع اي تايب {x:""} => تاخد دي مثلا لانها  قيمة من نوع {x:string}
//     LangStateValue,
// //export declare function on<State, Creators extends readonly ActionCreator[], InferredState = State>
// // (...args:
// // [ 
// // ...creators: Creators,
// //  reducer: OnReducer<State extends infer S ? S : never, Creators, InferredState> 
// // ]
// // ):ReducerTypes<unknown extends State ? InferredState : State, Creators>;


//     on(langAction,
//         (state, { lang }) => ({ ...state, currentLang: lang }))
//     )
