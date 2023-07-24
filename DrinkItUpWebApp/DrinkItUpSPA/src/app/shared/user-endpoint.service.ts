import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel, UserRegisterModel } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserEndpointService {
  private baseURL = 'https://localhost:7073/api/UserAPI'
  constructor(private http: HttpClient) {

   }


   getAll(){
    return this.http.get<UserModel[]>(`${this.baseURL}/GetAll`);
   }

   getAllCount(){
    return this.http.get<number>(`${this.baseURL}/GetAllCount`);
   }

   register(user: UserRegisterModel){
    return this.http.post(this.baseURL + '/register', user)
   }
}