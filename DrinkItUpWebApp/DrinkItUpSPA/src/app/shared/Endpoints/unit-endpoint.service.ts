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
}
