import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Forecast, WeatherModel } from '../Models/weather.model';

@Injectable({
  providedIn: 'root'
})
export class WeatherEndpointService {
  base: string = 'http://api.weatherapi.com/v1/forecast.json?key=14e5b362b83640ac979203727231107&q='
  rest: string = '&days=3&aqi=no&alerts=no'
  constructor(private http: HttpClient) { }

  public getWeather(city: string){
    return this.http.get<WeatherModel>(this.base + city + this.rest)
  }
}
