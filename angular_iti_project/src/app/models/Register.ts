export interface IRegister {
    displayName: IDisplayName,
    address: IAddress,
    phoneNumber: string[],
    email: string,
    password: string,
    confirmPassword: string
}
export interface ILogin {
    UserOrEmail: string,
    Password: string,
}
interface IDisplayName {
    displayFirstName: string,
    displayLastName: string
}
interface IAddress {
    firstName: string,
    lastName: string,
    street: string,
    city: string,
    country: string
}
export interface IRegisterValue {
    userName: string,
    email: string,

    // token: string,
    errors: string[]
}
export interface ILoginValue {
    token: string,
    error: string,
}
// {
//   "displayName": {
//     "displayFirstName": "string",
//     "displayLastName": "string"
//   },
//   "address": {
//     "firstName": "string",
//     "lastName": "string",
//     "street": "string",
//     "city": "string",
//     "country": "string"
//   },
//   "phoneNumber": [
//     "string"
//   ],
//   "email": "user@example.com",
//   "password": "string",
//   "confirmPassword": "string"
// }