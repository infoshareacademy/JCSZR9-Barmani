import { EventEmitter, Injectable } from "@angular/core";
import { Subject } from "rxjs";


@Injectable({providedIn: 'root'})
export class AddonsService{
    activateWeather = new Subject<boolean>();
    activateShopping = new Subject<boolean>();
}