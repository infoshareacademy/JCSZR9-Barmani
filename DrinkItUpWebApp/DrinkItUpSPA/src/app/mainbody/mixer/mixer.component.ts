import { KeyValuePipe } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { Subject, take, takeUntil, tap } from 'rxjs';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';
import { MixerService } from 'src/app/shared/mixer.service';

@Component({
  selector: 'app-mixer',
  templateUrl: './mixer.component.html',
  styleUrls: ['./mixer.component.css']
})
export class MixerComponent implements OnDestroy {
allIngredientsNames: string[] = [];
ingredientNames: string[] = [];
chosenIngredientsNames: string[] = [];
destroyed$ = new Subject();
//results = new Map<string,DrinkSearchModel[]>();
results: {[key: string]:DrinkSearchModel[]} = {};
resultKeys = Object.keys(this.results);
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

  this.mixerService.resultsSub.pipe( 
    tap( data => this.results = data),
    tap( _=> this.resultKeys = Object.keys(this.results)),
    takeUntil(this.destroyed$)
  ).subscribe();

  this.mixerService.getAllNames();

}
ngOnDestroy(): void {
    this.destroyed$.complete();
  }
;

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
