import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { DrinkEndpointService } from 'src/app/shared/drink-endpoint.service';
import { IngredientEndpointService } from 'src/app/shared/ingredient-endpoint.service';
import { UserEndpointService } from 'src/app/shared/user-endpoint.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent {

  users: number = 0;
  ingredients: number = 0;
  drinks: number = 0;

  constructor(private drinkEndpoint: DrinkEndpointService,
    private ingredientEndpoint: IngredientEndpointService,
    private userEndpoint: UserEndpointService){

      this.drinkEndpoint.getAll().pipe(
        tap(data => this.drinks = data.length)
      )
      .subscribe()

      this.userEndpoint.getAllCount().pipe(
        tap(data => this.users = data)
      )
      .subscribe()

      this.ingredientEndpoint.getAll().pipe(
        tap(data => this.ingredients = data.length)
      )
      .subscribe()
    }

}
