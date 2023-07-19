import { Component } from '@angular/core';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';

@Component({
  selector: 'app-by-category',
  templateUrl: './by-category.component.html',
  styleUrls: ['./by-category.component.css']
})
export class ByCategoryComponent {
results =new Map<string,DrinkSearchModel[]>();
formValue: string = 'Wszystkie drinki';
}
