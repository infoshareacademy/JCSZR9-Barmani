import { Injectable } from '@angular/core';
import { DrinkEndpointService } from '../Endpoints/drink-endpoint.service';
import { Subject } from 'rxjs';
import { IngredientEndpointService } from '../Endpoints/ingredient-endpoint.service';
import { DrinkSearchModel } from '../Models/drinkSearch.model';

@Injectable({
  providedIn: 'root'
})
export class MixerService {

  constructor(private drinkEnpoint: DrinkEndpointService, private ingredientEndpoint: IngredientEndpointService) { 
      
  }

  ingredientsNamesSub = new Subject<string[]>();
  chosenIngredientsNamesSub = new Subject<string[]>();
  resultsSub= new Subject<{[key:string]:DrinkSearchModel[]}>();
  autoCompleteIngredientsSub = new Subject<string[]>();

  public chosenIngredientsNames: string[] = [];
  private ingredientNames: string[] = [];
  private results: {[key:string]:DrinkSearchModel[]} = {};


getAutoComplete(input:string){
  console.log('up?')
  let chosen = this.chosenIngredientsNames.join(',');
  console.log('chosen:'+chosen)
  if(chosen === '')
  {
    chosen = 'ygryt'
  }
  if(input === '')
  {
    input = 'nothing'
  }
  this.drinkEnpoint.autoCompleteMixer(input,chosen).subscribe(data =>{
    this.autoCompleteIngredientsSub.next(data as string[]);
  });
}

addIngredient(ingredientName: string){
  this.chosenIngredientsNames.push(ingredientName);
  this.chosenIngredientsNamesSub.next(this.chosenIngredientsNames);
}

removeIngredient(ingredientName: string)
{
  this.chosenIngredientsNames.forEach((element,index)=>{
    if(element === ingredientName)
    {
      this.chosenIngredientsNames.splice(index, 1);
    }
  });

  this.chosenIngredientsNamesSub.next(this.chosenIngredientsNames);
}

searchByIngredients(){
  this.drinkEnpoint.searchByIngredients(this.chosenIngredientsNames.join()).subscribe(data =>{
    this.results = data; 
    this.resultsSub.next(this.results);
    });
    
  }

  getAllNames(){
    this.ingredientEndpoint.getAllNames().subscribe( data =>{
      this.ingredientNames = data
      this.ingredientsNamesSub.next(this.ingredientNames);
    }) 
  }
}
