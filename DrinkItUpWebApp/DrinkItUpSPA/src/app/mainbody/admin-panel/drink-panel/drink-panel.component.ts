import { Component, OnDestroy } from '@angular/core';
import { first, tap } from 'rxjs';
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
  
  private file!: File;

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

onFileChoose(event: any){
  if (event.length === 0) {
    return;
  }
  
  this.file = event.target.files[0];
}
uploadFoto(fileName: string){
  
  const formData = new FormData();
  formData.append('file', this.file, fileName)

  this.drinkEndpoint.upload(formData).pipe(tap(_=>{})).subscribe()
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
  let drink = this.adminService.drink;
  drink.mainAlcohol = this.mainAlcohols.find(mA => mA.mainAlcoholId === drink.mainAlcoholId);
  drink.difficulty = this.difficulties.find(d => d.difficultyId === drink.difficultyId);

  this.drinkEndpoint.add(drink).pipe(
    tap(data => drink = data),
    tap(_ => console.log(drink)), 
    tap(_ =>{
          this.uploadFoto(drink.drinkPhotoId!);
        }),
    tap(_ => this.getAll()),
    tap(_ => this.file!)
    )
    .subscribe()

}

onEditClick(id: number){
 this.drinkEndpoint.getById(id).pipe(
  tap(data => this.adminService.drink = data),
  tap(_ => this.drink = this.adminService.drink),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.drinkEndpoint.getById(id).pipe(
    tap(data => this.adminService.drink = data),
    tap(_ => this.drink = this.adminService.drink),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  let drink = this.adminService.drink;
  drink.mainAlcohol = this.mainAlcohols.find(mA => mA.mainAlcoholId === drink.mainAlcoholId);
  drink.difficulty = this.difficulties.find(d => d.difficultyId === drink.difficultyId);

  this.drinkEndpoint.update(drink).pipe(
    tap(_ => {
      if(this.file)
      {
        this.uploadFoto(drink.drinkPhotoId!);
      }
    }),
    tap(data => this.drink = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.drink.drinkId!;
  this.drinkEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.adminService.drink = {drinkId: 0, name: '', mainAlcoholId: 0, difficultyId: 0, description: '', drinkPhotoId: '', ingredients: []}),
    tap(_ => this.drink = this.adminService.drink),
  )
  .subscribe();
}
}
