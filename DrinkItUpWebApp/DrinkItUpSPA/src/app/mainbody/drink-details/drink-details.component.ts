import { Component } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { DrinkEndpointService } from 'src/app/shared/drink-endpoint.service';
import { DrinkModel } from 'src/app/shared/drink.model';

@Component({
  selector: 'app-drink-details',
  templateUrl: './drink-details.component.html',
  styleUrls: ['./drink-details.component.css']
})
export class DrinkDetailsComponent {

  drink!: DrinkModel;

  constructor(private drinkEndpoint: DrinkEndpointService, private route: ActivatedRoute){
    this.route.data.subscribe( (data: Data) => {
      this.drink = data['drink'];
    })
    
  }
}