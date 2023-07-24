import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DrinkModel } from './drink.model';
import { DrinkSearchModel } from './drinkSearch.model';

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

   searchByNameOrOneIngredient(input: string){
    return this.http.get(this.baseURL+'/search/'+input);
   };

   autoCompleteMixer(input: string, chosen: string){
    return this.http.get(this.baseURL+'/mixer/autocompleteingredient/'+ input + '/' + chosen);
   };

   getAll(){
    return this.http.get<DrinkSearchModel[]>(this.baseURL+'/GetAll');
   };


   
   getById(id: number){
    return this.http.get<DrinkModel>(this.baseURL+'/'+id);
   };

   searchByIngredients(searchNames: string){
    return this.http.get<{[key: string]:DrinkSearchModel[]}>(this.baseURL+'/mixer/'+searchNames);
   };

   getByMainAlcoholId(id: number){
    return this.http.get<DrinkSearchModel[]>(this.baseURL+'/byCategory/alcohol/'+id);
   };

   getByDifficultyId(id: number){
    return this.http.get<DrinkSearchModel[]>(this.baseURL+'/byCategory/difficulty/'+id);
   };
}

