import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { DrinkModel } from "../Models/drink.model";
import { DrinkEndpointService } from "../Endpoints/drink-endpoint.service";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class DrinkResolver implements Resolve<DrinkModel>{
    constructor(private drinkEndopoint: DrinkEndpointService){}

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): DrinkModel | Observable<DrinkModel> | Promise<DrinkModel> {
        return this.drinkEndopoint.getById(route.params['id']);
    }
}