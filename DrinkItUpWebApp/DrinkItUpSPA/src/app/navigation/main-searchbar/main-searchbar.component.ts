import { Component } from '@angular/core';
import { DrinkEndpointService } from 'src/app/shared/drink-endpoint.service';
import { DrinkSearchModel } from 'src/app/shared/drinkSearch.model';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-main-searchbar',
  templateUrl: './main-searchbar.component.html',
  styleUrls: ['./main-searchbar.component.css']
})
export class MainSearchbarComponent {
  constructor(private drinkEndpoint: DrinkEndpointService){};

isVisible = false;
inputSearch: string = '';
results: DrinkSearchModel[] = [];

onKeyUp(input: string){
    if(input === '')
    {
      this.isVisible = false;
      return;
    }
      
     this.drinkEndpoint.autoCompleteMain(input).subscribe( data =>{
      this.results = data as DrinkSearchModel[];
      this.isVisible = true;
      console.log(data);
     });
}
}
