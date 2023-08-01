import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RoleModel } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class RoleEndpointService {

  private baseURL = 'https://localhost:7073/api/RoleAPI'
  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<RoleModel[]>(`${this.baseURL}/GetAll`);
   }

   add(role: RoleModel){
    return this.http.post(this.baseURL+'/Add', role)
   }

   getById(id:number){
    return this.http.get<RoleModel>(this.baseURL+'/GetById/'+id);
   };

   update(upadatedRole: RoleModel){
    return this.http.post<RoleModel>(this.baseURL+'/Update',upadatedRole);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }
}
