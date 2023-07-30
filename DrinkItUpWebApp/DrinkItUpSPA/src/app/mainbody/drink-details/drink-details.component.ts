import { Component } from '@angular/core';
import { ActivatedRoute, Data } from '@angular/router';
import { DrinkEndpointService } from 'src/app/shared/Endpoints/drink-endpoint.service';
import { DrinkModel } from 'src/app/shared/Models/drink.model';
import { AddonsService } from 'src/app/shared/Services/addons.service';

@Component({
  selector: 'app-drink-details',
  templateUrl: './drink-details.component.html',
  styleUrls: ['./drink-details.component.css']
})
export class DrinkDetailsComponent {

  drink!: DrinkModel;

  constructor(private drinkEndpoint: DrinkEndpointService, private route: ActivatedRoute, public addonService: AddonsService){
    this.route.data.subscribe( (data: Data) => {
      this.drink = data['drink'];
    })
    
  }
}