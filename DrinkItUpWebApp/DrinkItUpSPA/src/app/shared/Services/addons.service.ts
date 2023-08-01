import { EventEmitter, Injectable } from "@angular/core";
import { Subject, tap } from "rxjs";
import { AuthenticationService } from "./authentication.service";
import { ShopingModel } from "../Models/shopingcart.model";
import { DrinkEndpointService } from "../Endpoints/drink-endpoint.service";


@Injectable({providedIn: 'root'})
export class AddonsService{
    activateWeather = new Subject<boolean>();
    activateShopping = new Subject<boolean>();
    shopingItem = new ShopingModel();
    shopingList: ShopingModel[] = [];

    isShown = false;
    isLogged = false;
    constructor(private authService: AuthenticationService, private drinkEndpoint: DrinkEndpointService){
    
      this.authService.user.pipe(
        tap(_ => this.userValue),
        tap(_ => this.isLogged= true)
      )
      .subscribe();
    }
    
    
    get userValue(){
      const user = this.authService.userValue
      if(user)
      {
        this.isLogged = true;
        return user;
      }
      this.isLogged = false;
      return null;
    }

    onAddItem(ingredient: string,quantity: number, unit: string){
        this.shopingItem.ingredient = ingredient;
        this.shopingItem.quantity = quantity;
        this.shopingItem.unit = unit;
        this.isShown = true;
    }

    addItem()
    {
        this.shopingList.forEach((element,index) =>{
            if(element.ingredient === this.shopingItem.ingredient)
            {
                element.quantity = element.quantity! + this.shopingItem.quantity!
                this.shopingItem.quantity = 0;
            }
        })
        if(this.shopingItem.quantity! > 0)
        {
            this.shopingList.push(this.shopingItem);
        }
        this.shopingItem = new ShopingModel();
        this.isShown = false;
    }

    removeItem(index: number)
    {
        this.shopingList.splice(index,1);
    }

    onClose()
    {
        this.isShown = false;
    }

    sendList(){
        let message:string[] = [];
        this.shopingList.forEach((element)=>{
            message.push(element.ingredient!+': ' + element.quantity! + element.unit+'\n')
        })
        this.drinkEndpoint.sendEmail(this.userValue?.email!,message.join(', ')).subscribe(_=>{});
        this.shopingList = [];
        this.isShown = false;
    }

    
}