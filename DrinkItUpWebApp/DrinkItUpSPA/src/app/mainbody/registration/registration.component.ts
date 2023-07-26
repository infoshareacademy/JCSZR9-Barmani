import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/Services/authentication.service';
import { UserEndpointService } from 'src/app/shared/Endpoints/user-endpoint.service';
import { UserModel, UserRegisterModel } from 'src/app/shared/Models/user.model';
import { UserService } from 'src/app/shared/Services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  constructor(private userService: UserService, private auth: AuthenticationService){}
email: string = '';
password: string = '';
confirmPassword: string = '';
dateOfBirth: Date = new Date();
userNameToShow:string = ''

onSubmit(){
let user: UserRegisterModel = new UserRegisterModel(this.userNameToShow,this.email,this.password,this.dateOfBirth);
this.userService.register(user);
}


}
