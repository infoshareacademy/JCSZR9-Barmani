import { Component } from '@angular/core';
import { first, tap } from 'rxjs';
import { DifficultyEndpointService } from 'src/app/shared/Endpoints/difficulty-endpoint.service';
import { DifficultyModel } from 'src/app/shared/Models/difficulty.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-difficulty-panel',
  templateUrl: './difficulty-panel.component.html',
  styleUrls: ['./difficulty-panel.component.css']
})
export class DifficultyPanelComponent {
  loaded = false;
  isEdited = false;
  difficulties: DifficultyModel[] = [];
  difficulty: DifficultyModel = new DifficultyModel();

constructor(public adminService: AdminPanelService,private difficultyEndpoint: DifficultyEndpointService){
 this.getAll();
}

getAll(){
  this.difficultyEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.difficulties = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

onAddClick(){
  let difficultyToAdd = new DifficultyModel();
  difficultyToAdd.name = this.difficulty.name;
  this.difficultyEndpoint.add(difficultyToAdd).pipe(
    tap(data => this.difficulty = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onEditClick(id: number){
 this.difficultyEndpoint.getById(id).pipe(
  tap(data => this.difficulty = data),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.difficultyEndpoint.getById(id).pipe(
    tap(data => this.difficulty= data),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  this.difficultyEndpoint.update(this.difficulty).pipe(
    tap(data => this.difficulty = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.difficulty.difficultyId!;
  this.difficultyEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.difficulty = new DifficultyModel())
  )
  .subscribe();
}
}
