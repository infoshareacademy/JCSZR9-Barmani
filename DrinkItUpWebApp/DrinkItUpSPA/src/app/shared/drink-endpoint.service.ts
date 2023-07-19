import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DrinkModel } from './drink.model';

@Injectable({
  providedIn: 'root'
})
export class DrinkEndpointService {
  private baseURL = 'https://localhost:7073/api/DrinkAPI'
  constructor(private http: HttpClient) {

   }

   autoCompleteMain(input: string){
    return this.http.get(this.baseURL+'/autocompletemain/'+input);
   };

   autoCompleteMixer(input: string, chosen: string){
    return this.http.get(this.baseURL+'/mixer/autocompleteingredient/'+ input + '/' + chosen);
   };


   
   getById(id: number){
    return this.http.get<DrinkModel>(this.baseURL+'/'+id);
   }

   searchByIngredients(searchNames: string){
    return this.http.get(this.baseURL+'/mixer/'+searchNames);
   }
}

