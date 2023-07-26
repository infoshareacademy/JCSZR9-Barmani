import { Component } from '@angular/core';
import { map, tap } from 'rxjs';
import { WeatherEndpointService } from 'src/app/shared/Endpoints/weather-endpoint.service';
import { Forecast, WeatherModel } from 'src/app/shared/Models/weather.model';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html',
  styleUrls: ['./weather.component.css']
})
export class WeatherComponent {
  weatherLoaded = false;
  city:string = '';
  forecast: any = {};
  constructor(private weatherEndpoint: WeatherEndpointService){}
onClick(){
  this.weatherEndpoint.getWeather(this.city).pipe(
    map((data: WeatherModel) => {
      this.forecast = data;
    }),
    tap(_ => this.weatherLoaded = true)
  )
  .subscribe(_ => {
    console.log('helllo' + this.forecast)
  });
}
}
