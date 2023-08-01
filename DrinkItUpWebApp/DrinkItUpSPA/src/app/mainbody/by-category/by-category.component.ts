import { Component } from '@angular/core';
import { DifficultyEndpointService } from 'src/app/shared/Endpoints/difficulty-endpoint.service';
import { DifficultyModel } from 'src/app/shared/Models/difficulty.model';
import { DrinkEndpointService } from 'src/app/shared/Endpoints/drink-endpoint.service';
import { DrinkSearchModel } from 'src/app/shared/Models/drinkSearch.model';
import { MainAlcoholEndpointService } from 'src/app/shared/Endpoints/main-alcohol-endpoint.service';
import { MainAlcoholModel } from 'src/app/shared/Models/mainAlcohol.model';

@Component({
  selector: 'app-by-category',
  templateUrl: './by-category.component.html',
  styleUrls: ['./by-category.component.css']
})
export class ByCategoryComponent {
formValue: string = 'Wszystkie drinki';
difficulties:DifficultyModel[] = [];
mainAlcohols:MainAlcoholModel[] = [];
results: DrinkSearchModel[] = [];
chosen: string = '';

constructor(private drinkEndpoint: DrinkEndpointService,
  private mainAlcoholEndpoint: MainAlcoholEndpointService,
  private difficultyEndpoint: DifficultyEndpointService){
  this.drinkEndpoint.getAll().subscribe(data =>{
    this.results = data as DrinkSearchModel[];
  })

  this.mainAlcoholEndpoint.getAll().subscribe( data =>{
    this.mainAlcohols = data;
  })

  this.difficultyEndpoint.getAll().subscribe( data => {
    this.difficulties = data;
  })

}

onCategoryChoose(){
  if(this.formValue === 'Wszystkie drinki')
  {
    this.drinkEndpoint.getAll().subscribe(data =>{
      this.results = data as DrinkSearchModel[];
    })
  }

  if(this.formValue === 'Poziom trudności')
  {
    let difficulty = this.difficulties.at(0);
    this.chosen = difficulty?.name as string;
    this.drinkEndpoint.getByDifficultyId(difficulty?.difficultyId as number).subscribe(data => {
      this.results = data;
    })
  }

  if(this.formValue === 'Alkohol dominujący')
  {
    let mainAlcohol = this.mainAlcohols.at(0);
    this.chosen = mainAlcohol?.name as string;
    this.drinkEndpoint.getByMainAlcoholId(mainAlcohol?.mainAlcoholId as number).subscribe(data => {
      this.results = data;
    })
  }
}

onMainAlcoholChoose(mainAlcohol: MainAlcoholModel){
  this.drinkEndpoint.getByMainAlcoholId(mainAlcohol.mainAlcoholId!).subscribe(data =>{
    this.results = data;
    this.chosen = mainAlcohol.name!;
  })
}

onDifficultyChoose(difficulty: DifficultyModel){
  this.drinkEndpoint.getByDifficultyId(difficulty.difficultyId!).subscribe(data =>{
    this.results = data;
    this.chosen = difficulty.name!;
  })
}
}
