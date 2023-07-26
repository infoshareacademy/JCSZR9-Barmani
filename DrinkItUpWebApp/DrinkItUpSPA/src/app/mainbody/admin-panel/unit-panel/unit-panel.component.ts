import { Component } from '@angular/core';
import { tap } from 'rxjs';
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
  this.unitEndpoint.add(this.unit).pipe(
    tap(data => this.unit = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

}
