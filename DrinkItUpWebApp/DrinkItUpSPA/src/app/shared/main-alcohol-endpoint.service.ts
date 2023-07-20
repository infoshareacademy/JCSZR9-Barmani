import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MainAlcoholModel } from './mainAlcohol.model';

@Injectable({
  providedIn: 'root'
})
export class MainAlcoholEndpointService {

  private baseURL = 'https://localhost:7073/api/MainAlcoholAPI'
  constructor(private http: HttpClient) {

   }

   getAll(){
    return this.http.get<MainAlcoholModel[]>(this.baseURL + '/GetAll');
   }
}
