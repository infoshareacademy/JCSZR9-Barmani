import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IngredientModel } from '../Models/ingredient.model';
import { UnitModel } from '../Models/unit.model';

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

   getAllUnitsForIngredient(ingredientName: string){
    return this.http.get<UnitModel[]>(this.baseURL+'/GetAllUnits/'+ ingredientName)
   }

   getByNameAndUnit(ingredientName: string, unitId: number){
    return this.http.get<IngredientModel>(this.baseURL+'/GetByNameAndUnit/'+ ingredientName + '/'+ unitId)
   }


   getAll(){
    return this.http.get<IngredientModel[]>(this.baseURL+'/GetAll');
   }

   add(ingredient: IngredientModel){
    return this.http.post<IngredientModel>(this.baseURL+'/Add', ingredient)
   }

   getById(id:number){
    return this.http.get<IngredientModel>(this.baseURL+'/GetById/'+id);
   };

   update(ingredientToUpdate: IngredientModel){
    return this.http.post<IngredientModel>(this.baseURL+'/Update',ingredientToUpdate);
   };

   delete(id: number){
    return this.http.delete<any>(this.baseURL+'/Delete/'+ id);
   }

}
