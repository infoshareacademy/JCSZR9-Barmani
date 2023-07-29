import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import { DrinkModel } from '../Models/drink.model';

@Injectable({
  providedIn: 'root'
})
export class AdminPanelService {
  categoryChoose$ = new Subject<string>();
  drink: DrinkModel = {drinkId: 0, name: '', mainAlcoholId: 0, difficultyId: 0, description: '', drinkPhotoId: '', ingredients: []}
  constructor() { }

  public onCategoryChoose(input: string){
    this.categoryChoose$.next(input);

  }

  getDrink = new Subject<DrinkModel>();
}
