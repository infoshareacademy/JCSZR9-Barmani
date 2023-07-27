import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DifficultyModel } from '../Models/difficulty.model';

@Injectable({
  providedIn: 'root'
})
export class DifficultyEndpointService {

  private baseURL = 'https://localhost:7073/api/DifficultyAPI'
  constructor(private http: HttpClient) {

   }

   getAll(){
    return this.http.get<DifficultyModel[]>(this.baseURL + '/GetAll');
   }

   add(difficulty: DifficultyModel){
    return this.http.post<DifficultyModel>(this.baseURL+'/Add', difficulty)
   }

   getById(id:number){
    return this.http.get<DifficultyModel>(this.baseURL+'/GetById/'+id);
   };

   update(difficultyToUpdate: DifficultyModel){
    return this.http.post<DifficultyModel>(this.baseURL+'/Update',difficultyToUpdate);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }
}
