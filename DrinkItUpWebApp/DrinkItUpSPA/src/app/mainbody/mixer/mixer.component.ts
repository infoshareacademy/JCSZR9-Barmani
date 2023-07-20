import { KeyValuePipe } from '@angular/common';
import { Component } from '@angular/core';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';
import { MixerService } from 'src/app/shared/mixer.service';

@Component({
  selector: 'app-mixer',
  templateUrl: './mixer.component.html',
  styleUrls: ['./mixer.component.css']
})
export class MixerComponent {
allIngredientsNames: string[] = [];
ingredientNames: string[] = [];
chosenIngredientsNames: string[] = [];

results = new Map<string,DrinkSearchModel[]>();
input:string = '';

constructor(private mixerService: MixerService){
  this.mixerService.autoCompleteIngredientsSub.subscribe((data: string[]) =>{
    this.ingredientNames = data
  });

  if(this.mixerService.chosenIngredientsNames.length ===0){
  this.mixerService.chosenIngredientsNamesSub.subscribe((data: string[]) =>{
    this.chosenIngredientsNames = data
  });
  }
  else{
    this.chosenIngredientsNames = this.mixerService.chosenIngredientsNames;
  }

  this.mixerService.ingredientsNamesSub.subscribe((data: string[]) =>{
    this.allIngredientsNames = data
  });

  this.mixerService.resultsSub.subscribe(data =>{
    let res = new Map<string,DrinkSearchModel[]>();
    res = data;
    console.log(res);
    let keys = Object.keys(res).sort().reverse();
    
    for(let key of keys)
    {

    }
    this.results = res;
  });

  this.mixerService.getAllNames();
};

onKeyUp(input: string){

  this.mixerService.getAutoComplete(input);
}

addIngredient(ingredientName: string){
  this.mixerService.addIngredient(ingredientName);
  this.mixerService.getAutoComplete(this.input);
  this.allIngredientsNames.forEach((element,index)=>{
    if(element === ingredientName)
    {
      this.allIngredientsNames.splice(index,1);
    }
  })
}

removeIngredient(ingredientName: string){
  this.mixerService.removeIngredient(ingredientName);
  this.mixerService.getAutoComplete(this.input);
  this.allIngredientsNames.push(ingredientName);
  this.allIngredientsNames.sort();

}

searchByIngredients(){
  this.mixerService.searchByIngredients();
}

}
