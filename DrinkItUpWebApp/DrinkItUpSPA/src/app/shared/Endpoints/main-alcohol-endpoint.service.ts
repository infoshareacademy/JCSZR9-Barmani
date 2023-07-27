import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MainAlcoholModel } from '../Models/mainAlcohol.model';

@Injectable({
  providedIn: 'root'
})
export class MainAlcoholEndpointService {

  private baseURL = 'https://localhost:7073/api/MainAlcoholAPI'
  constructor(private http: HttpClient) {

   }

   getAll(){
    return this.http.get<MainAlcoholModel[]>(this.baseURL + '/GetAll');
   }

   add(mainAlcohol: MainAlcoholModel){
    return this.http.post<MainAlcoholModel>(this.baseURL+'/Add', mainAlcohol)
   }

   getById(id:number){
    return this.http.get<MainAlcoholModel>(this.baseURL+'/GetById/'+id);
   };

   update(mainAlcoholToUpdate: MainAlcoholModel){
    return this.http.post<MainAlcoholModel>(this.baseURL+'/Update',mainAlcoholToUpdate);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }
}
