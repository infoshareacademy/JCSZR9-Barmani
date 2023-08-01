import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UnitModel } from '../Models/unit.model';

@Injectable({
  providedIn: 'root'
})
export class UnitEndpointService {
  private baseURL = 'https://localhost:7073/api/UnitAPI'
  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<UnitModel[]>(`${this.baseURL}/GetAll`);
   }

   add(unit: UnitModel){
    return this.http.post(this.baseURL+'/Add', unit)
   }

   getById(id:number){
    return this.http.get<UnitModel>(this.baseURL+'/GetById/'+id);
   };

   update(upadatedUnit: UnitModel){
    return this.http.post<UnitModel>(this.baseURL+'/Update',upadatedUnit);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }
}

