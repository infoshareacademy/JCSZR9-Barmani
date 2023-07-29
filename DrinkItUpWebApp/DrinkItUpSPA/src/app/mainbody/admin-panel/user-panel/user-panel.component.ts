import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { RoleEndpointService } from 'src/app/shared/Endpoints/role-endpoint.service';
import { UserEndpointService } from 'src/app/shared/Endpoints/user-endpoint.service';
import { UserModel } from 'src/app/shared/Models/user.model';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent {
  loaded = false;
  isEdited = false;
  users: UserModel[] = [];

constructor(public adminService: AdminPanelService,private userEndpoint: UserEndpointService, private roleEndpoint: RoleEndpointService){
 this.getAll();
}

getAll(){
  this.userEndpoint.getAll().pipe(
    tap(_ => this.loaded = false),
    tap( data => this.users = data),
    tap( _ => this.loaded = true)
   )
   .subscribe(_ =>{
    this.users.forEach((element,index)=>{
      this.roleEndpoint.getById(element.roleId!).pipe(
        tap(data => element.role = data)
      )
      .subscribe();
    })
   });
}
}
