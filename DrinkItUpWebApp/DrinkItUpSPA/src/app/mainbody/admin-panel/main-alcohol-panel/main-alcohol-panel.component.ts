import { Component } from '@angular/core';
import { first, tap } from 'rxjs';
import { MainAlcoholEndpointService } from 'src/app/shared/Endpoints/main-alcohol-endpoint.service';
import { MainAlcoholModel } from 'src/app/shared/Models/mainAlcohol.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-main-alcohol-panel',
  templateUrl: './main-alcohol-panel.component.html',
  styleUrls: ['./main-alcohol-panel.component.css']
})
export class MainAlcoholPanelComponent {
  loaded = false;
  isEdited = false;
  mainAlcohols: MainAlcoholModel[] = [];
  mainAlcohol: MainAlcoholModel = new MainAlcoholModel();

constructor(public adminService: AdminPanelService,private mainAlcoholEndpoint: MainAlcoholEndpointService){
 this.getAll();
}

getAll(){
  this.mainAlcoholEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.mainAlcohols = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

onAddClick(){
  let unitToAdd = new MainAlcoholModel();
  unitToAdd.name = this.mainAlcohol.name;
  this.mainAlcoholEndpoint.add(unitToAdd).pipe(
    tap(data => this.mainAlcohol = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onEditClick(id: number){
 this.mainAlcoholEndpoint.getById(id).pipe(
  tap(data => this.mainAlcohol = data),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.mainAlcoholEndpoint.getById(id).pipe(
    tap(data => this.mainAlcohol= data),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  this.mainAlcoholEndpoint.update(this.mainAlcohol).pipe(
    tap(data => this.mainAlcohol = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.mainAlcohol.mainAlcoholId!;
  this.mainAlcoholEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.mainAlcohol = new MainAlcoholModel())
  )
  .subscribe();
}
}
