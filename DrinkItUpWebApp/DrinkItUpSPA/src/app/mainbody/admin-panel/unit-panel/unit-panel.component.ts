import { Component } from '@angular/core';
import { first, subscribeOn, tap } from 'rxjs';
import { UnitEndpointService } from 'src/app/shared/Endpoints/unit-endpoint.service';
import { UnitModel } from 'src/app/shared/Models/unit.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-unit-panel',
  templateUrl: './unit-panel.component.html',
  styleUrls: ['./unit-panel.component.css']
})
export class UnitPanelComponent {
  loaded = false;
  isEdited = false;
  units: UnitModel[] = [];
  unit: UnitModel = new UnitModel();

constructor(public adminService: AdminPanelService,private unitEndpoint: UnitEndpointService){
 this.getAll();
}

getAll(){
  this.unitEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.units = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

onAddClick(){
  let unitToAdd = new UnitModel();
  unitToAdd.name = this.unit.name;
  this.unitEndpoint.add(unitToAdd).pipe(
    tap(data => this.unit = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onEditClick(id: number){
 this.unitEndpoint.getById(id).pipe(
  tap(data => this.unit = data),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.unitEndpoint.getById(id).pipe(
    tap(data => this.unit = data),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  this.unitEndpoint.update(this.unit).pipe(
    tap(data => this.unit = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.unit.unitId!;
  this.unitEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.unit = new UnitModel())
  )
  .subscribe();
}

}
