import { Component, Input, OnInit } from '@angular/core';
import { AddonsService } from '../shared/Services/addons.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mainbody',
  templateUrl: './mainbody.component.html',
  styleUrls: ['./mainbody.component.css']
})
export class MainbodyComponent implements OnInit {
body: string = 'registration';
isWeatherOpen = false;
isShoppingOpen = false;
subscriptionWeather: Subscription = new Subscription();
subscriptionShopping: Subscription = new Subscription();
constructor(private addonsService: AddonsService){}

ngOnInit(): void {
  this.subscriptionWeather = this.addonsService.activateWeather.subscribe(addonActivation => {
    this.isWeatherOpen = addonActivation;
  });

  this.subscriptionShopping = this.addonsService.activateShopping.subscribe( addonActivation => {
    this.isShoppingOpen = addonActivation
  });
}

}
