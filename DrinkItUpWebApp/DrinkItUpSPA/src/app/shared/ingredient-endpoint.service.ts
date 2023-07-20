import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class IngredientEndpointService {

  private baseURL = 'https://localhost:7073/api/IngredientAPI'
  constructor(private http: HttpClient) {

   }
   
   getAllNames(){
    return this.http.get<string[]>(this.baseURL+'/GetAllNames');
   }
}
