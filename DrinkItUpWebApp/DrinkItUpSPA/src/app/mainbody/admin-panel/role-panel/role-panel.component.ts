import { Component } from '@angular/core';
import { first, tap } from 'rxjs';
import { RoleEndpointService } from 'src/app/shared/Endpoints/role-endpoint.service';
import { RoleModel } from 'src/app/shared/Models/user.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-role-panel',
  templateUrl: './role-panel.component.html',
  styleUrls: ['./role-panel.component.css']
})
export class RolePanelComponent {
  loaded = false;
  isEdited = false;
  roles: RoleModel[] = [];
  role: RoleModel = new RoleModel();

constructor(public adminService: AdminPanelService,private roleEndpoint: RoleEndpointService){
 this.getAll();
}

getAll(){
  this.roleEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.roles = data),
    tap( _ => this.loaded = true)
   )
   .subscribe();
}

onAddClick(){
  let roleToAdd = new RoleModel();
  roleToAdd.name = this.role.name;
  this.roleEndpoint.add(roleToAdd).pipe(
    tap(data => this.role = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onEditClick(id: number){
 this.roleEndpoint.getById(id).pipe(
  tap(data => this.role = data),
  tap(_ => this.isEdited = true)
 )
 .subscribe();
}

onDeleteClick(id: number){
  this.roleEndpoint.getById(id).pipe(
    tap(data => this.role = data),
    tap(_ => this.isEdited = true)
   )
   .subscribe();
}

onUpdate(){
  this.roleEndpoint.update(this.role).pipe(
    tap(data => this.role = data),
    tap(_ => this.getAll())
  )
  .subscribe();
}

onDelete(){
  
  let id = this.role.roleId!;
  this.roleEndpoint.delete(id).pipe(
    tap(_ => first()),
    tap(_ => this.getAll()),
    tap(_ => this.isEdited = false),
    tap(_ => this.role = new RoleModel())
  )
  .subscribe();
}
}
