import { Component } from '@angular/core';
import { first, tap } from 'rxjs';
import { IngredientEndpointService } from 'src/app/shared/Endpoints/ingredient-endpoint.service';
import { UnitEndpointService } from 'src/app/shared/Endpoints/unit-endpoint.service';
import { IngredientModel } from 'src/app/shared/Models/ingredient.model';
import { UnitModel } from 'src/app/shared/Models/unit.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-ingredient-panel',
  templateUrl: './ingredient-panel.component.html',
  styleUrls: ['./ingredient-panel.component.css']
})
export class IngredientPanelComponent {
  loaded = false;
  isEdited = false;
  ingredients: IngredientModel[] = [];
  ingredient: IngredientModel = new IngredientModel(new UnitModel());
  units: UnitModel[] = [];
constructor(public adminService: AdminPanelService,private ingredientEndpoint: IngredientEndpointService, private unitEndpoint: UnitEndpointService){
 this.getAll();
 this.getAllUnits();
}

getAll(){
  this.ingredientEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.ingredients = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

getAllUnits(){
  this.unitEndpoint.getAll().pipe(
    tap(data => this.units = data)
  )
  .subscribe();
}

onAddClick(){
  console.log(this.ingredient);
  let ingredientToAdd = new IngredientModel(new UnitModel());
  ingredientToAdd.name = this.ingredient.name;
  ingredientToAdd.unit = this.ingredient.unit ;
  ingredientToAdd.unitId = this.ingredient.unit.unitId;

  this.ingredientEndpoint.add(ingredientToAdd).pipe(
    tap(data => this.ingredient = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onEditClick(id: number){
 this.ingredientEndpoint.getById(id).pipe(
  tap(data => this.ingredient = data),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.ingredientEndpoint.getById(id).pipe(
    tap(data => this.ingredient= data),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  let ingredientToUpdate = new IngredientModel(new UnitModel());
  ingredientToUpdate.ingredientId = this.ingredient.ingredientId;
  ingredientToUpdate.name = this.ingredient.name;
  ingredientToUpdate.unitId = this.ingredient.unitId;
  ingredientToUpdate.unit = this.units.find(u => u.unitId === this.ingredient.unitId)!;
  console.log(this.units.find(u => u.unitId === this.ingredient.unitId))
  this.ingredientEndpoint.update(ingredientToUpdate).pipe(
    tap(data => this.ingredient = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.ingredient.ingredientId!;
  this.ingredientEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.ingredient = new IngredientModel(new UnitModel()))
  )
  .subscribe();
}
}
