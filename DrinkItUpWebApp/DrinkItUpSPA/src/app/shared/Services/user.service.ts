import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel, UserRegisterModel } from '../Models/user.model';
import { Subject } from 'rxjs';
import { UserEndpointService } from '../Endpoints/user-endpoint.service';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';



@Injectable({ providedIn: 'root' })
export class UserService {
  
  users:UserModel[] = [];
  usersSub = new Subject<UserModel[]>();
    constructor(private userEndpoint: UserEndpointService, private authenticationService: AuthenticationService, private router: Router) { }

    getAll() {
        this.userEndpoint.getAll().subscribe( data => {
          this.users = data;
        })

        this.usersSub.next(this.users);
    }

    register(user: UserRegisterModel){
      this.userEndpoint.register(user).subscribe( data =>{
        this.router.navigate(['/login']);
      })
    }
}