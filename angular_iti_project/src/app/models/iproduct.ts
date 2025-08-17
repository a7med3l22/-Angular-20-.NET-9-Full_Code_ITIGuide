export interface IProduct {
    id:number;
    name:string;
    categoryId:number;
    price:number;
    quantity:number;
    category?:string;
    pictureUrl:string;
    buyQuantity?:number;
}

export interface IAddProduct{
//   name: string |undefined| null;  النيم ي ياخد استرنج او نال او ان ديفيند + ممكن متبعتهوش خالص ولازم تبعته وتحدد قيمة م الثلاثه 
//   name?: string | null; النيم ي ياخد استرنج او نال او ان ديفيند + ممكن متبعتهوش خالص
//  // اعمل اللي انتا عاوزة ع حسب المنطق بتاعك

  name: string ;
  categoryId: number;
  price: number;
  quantity: number;
  pictureFile: File;
}
export interface IUpdateProduct {
  ProductId:number;
  name: string;
  categoryId: number;
  price: number;
  quantity: number;
  pictureFile?: File|null;
}


// متعدلش ف الكود وسع واعمل كمبوننت وموديل يستقبلوا من ال باك بتاعك انما متعدلش ابدا علي حاجة عملتها وتعك الدنيا 

// عاش والله انا عدلت ف الباك وخليته يبعتلي كده وشغال تمام و ف داتا بيز لوحدها كمان عاش اوي 
// [
//   {
//     "id": 0,
//     "name": "string",
//     "categoryId": 0,
//     "price": 0,
//     "quantity": 0,
//     "category": "string",
//     "pictureUrl": "string"
//   }
// ]