import { Component } from '@angular/core';
import { DifficultyModel } from 'src/app/shared/difficulty.model';
import { DrinkEndpointService } from 'src/app/shared/drink-endpoint.service';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';
import { MainAlcoholModel } from 'src/app/shared/mainAlcohol.model';

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

constructor(private drinkEndpoint: DrinkEndpointService){
  this.drinkEndpoint.getAll().subscribe(data =>{
    this.results = data as DrinkSearchModel[];
  })
}
}
