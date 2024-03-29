import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DrinkModel } from '../Models/drink.model';
import { DrinkSearchModel } from '../Models/drinkSearch.model';
import { IngredientModel } from '../Models/ingredient.model';

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

   upload(form: FormData){
    return this.http.post(this.baseURL+'/Upload',form);
   }

   add(drink: DrinkModel){
    return this.http.post<DrinkModel>(this.baseURL+'/Add', drink)
   }


   update(drinkToUpdate: DrinkModel){
    return this.http.post<DrinkModel>(this.baseURL+'/Update',drinkToUpdate);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }

   sendEmail(email:string,message:string){
    return this.http.post<any>(this.baseURL + '/SendList',{email: email, message: message});
   }
}

