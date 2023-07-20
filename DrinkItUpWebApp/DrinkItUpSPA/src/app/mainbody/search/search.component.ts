import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DrinkEndpointService } from 'src/app/shared/drink-endpoint.service';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  results: DrinkSearchModel[] = [];
  constructor(private drinkEndpoint: DrinkEndpointService, private route: ActivatedRoute){
    this.drinkEndpoint.searchByNameOrOneIngredient(route.snapshot.params['input'] as string).subscribe( data  => {
      this.results = data as DrinkSearchModel[];
    })
  }

}
