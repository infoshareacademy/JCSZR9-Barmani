import { Component, OnDestroy } from '@angular/core';
import { tap } from 'rxjs';
import { DifficultyEndpointService } from 'src/app/shared/Endpoints/difficulty-endpoint.service';
import { DrinkEndpointService } from 'src/app/shared/Endpoints/drink-endpoint.service';
import { IngredientEndpointService } from 'src/app/shared/Endpoints/ingredient-endpoint.service';
import { MainAlcoholEndpointService } from 'src/app/shared/Endpoints/main-alcohol-endpoint.service';
import { UnitEndpointService } from 'src/app/shared/Endpoints/unit-endpoint.service';
import { DifficultyModel } from 'src/app/shared/Models/difficulty.model';
import { DrinkModel } from 'src/app/shared/Models/drink.model';
import { IngredientModel } from 'src/app/shared/Models/ingredient.model';
import { MainAlcoholModel } from 'src/app/shared/Models/mainAlcohol.model';
import { UnitModel } from 'src/app/shared/Models/unit.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-drink-panel',
  templateUrl: './drink-panel.component.html',
  styleUrls: ['./drink-panel.component.css']
})
export class DrinkPanelComponent implements OnDestroy {
  loaded = false;
  isEdited = false;
  isLoadedIngredient = false;
  ingredient: IngredientModel = new IngredientModel(new UnitModel());
  drinks: DrinkModel[] = [];
  drink: DrinkModel = this.adminService.drink;
  
  units: UnitModel[] = [];
  mainAlcohols: MainAlcoholModel[] = [];
  difficulties: DifficultyModel[] = [];
  ingredients: string[] = [];
constructor(public adminService: AdminPanelService,
  private drinkEndpoint: DrinkEndpointService,
  private ingredientEndpoint: IngredientEndpointService, 
  private unitEndpoint: UnitEndpointService,
  private mainAlcoholEndpoint: MainAlcoholEndpointService,
  private difficultyEndpoint: DifficultyEndpointService){
 this.getAll();
 this.drink.ingredients = this.adminService.drink.ingredients;
 this.ingredientEndpoint.getAllNames().pipe(
  tap(data => this.ingredients = data)
 )
 .subscribe()


 this.difficultyEndpoint.getAll().pipe(
  tap(data => this.difficulties = data)
 )
 .subscribe()

 this.mainAlcoholEndpoint.getAll().pipe(
  tap(data => this.mainAlcohols = data)
 )
 .subscribe()

}
  ngOnDestroy(): void {
    this.adminService.drink = this.drink;
    console.log(this.adminService.drink)
  }

getAll(){
  this.drinkEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.drinks = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

onAddIngredientClick(){
  let quantity = this.ingredient.quantity;
  let ingredientToAdd = new IngredientModel(new UnitModel());
  
  
  

  this.ingredientEndpoint.getByNameAndUnit(this.ingredient.name!, this.ingredient.unitId!).pipe(
    tap(data => ingredientToAdd = data),
    tap(_ => ingredientToAdd.quantity = quantity),
    tap(_=> console.log(this.drink.ingredients)),
    tap(_=> this.ingredient = {ingredientId: 0, name: '', unitId: 0, unit: {unitId: 0, name: ''}, quantity: 0, isUsed: false}),
  )
  .subscribe();

  this.unitEndpoint.getById(this.ingredient.unitId!).pipe(
    tap(data => ingredientToAdd.unit = data),
    tap(_=> this.drink.ingredients?.push(ingredientToAdd)),
    tap(_=> {
      this.ingredients.forEach((element,index) => {
        if(element === ingredientToAdd.name)
        {
          this.ingredients.splice(index,1);
        }
      });
    })
  )
  .subscribe();

}
removeIngredient(index: number){
  let name = this.drink!.ingredients!.at(index)!.name;
  this.ingredients.push(name!);
  this.ingredients.sort();
  this.drink.ingredients?.splice(index, 1);
}

onIngredientChoose(){
  this.ingredientEndpoint.getAllUnitsForIngredient(this.ingredient.name!).pipe(
    tap(_=> this.isLoadedIngredient = false),
    tap(data => this.units = data),
    tap(_=> this.isLoadedIngredient = true),
  )
  .subscribe();
}
onAddClick(){
//   console.log(this.drink);
//   let ingredientToAdd = new IngredientModel(new UnitModel());
//   ingredientToAdd.name = this.drink.name;
//   ingredientToAdd.unit = this.drink.unit ;
//   ingredientToAdd.unitId = this.drink.unit.unitId;

//   this.ingredientEndpoint.add(ingredientToAdd).pipe(
//     tap(data => this.drink = data),
//     tap(_ => this.getAll())
//   )
//   .subscribe();
}

// onEditClick(id: number){
//  this.ingredientEndpoint.getById(id).pipe(
//   tap(data => this.drink = data),
//   tap(_ => this.isEdited = true)
//  )
//  .subscribe();
// }

// onDeleteClick(id: number){
//   this.ingredientEndpoint.getById(id).pipe(
//     tap(data => this.drink= data),
//     tap(_ => this.isEdited = true)
//    )
//    .subscribe();
// }

// onUpdate(){
//   let ingredientToUpdate = new IngredientModel(new UnitModel());
//   ingredientToUpdate.ingredientId = this.drink.ingredientId;
//   ingredientToUpdate.name = this.drink.name;
//   ingredientToUpdate.unitId = this.drink.unitId;
//   ingredientToUpdate.unit = this.units.find(u => u.unitId === this.drink.unitId)!;
//   console.log(this.units.find(u => u.unitId === this.drink.unitId))
//   this.ingredientEndpoint.update(ingredientToUpdate).pipe(
//     tap(data => this.drink = data),
//     tap(_ => this.getAll())
//   )
//   .subscribe();
// }

// onDelete(){
  
//   let id = this.drink.ingredientId!;
//   this.ingredientEndpoint.delete(id).pipe(
//     tap(_ => first()),
//     tap(_ => this.getAll()),
//     tap(_ => this.isEdited = false),
//     tap(_ => this.drink = new IngredientModel(new UnitModel()))
//   )
//   .subscribe();
// }
}
