import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/authentication.service';

@Component({
  selector: 'app-user-dropdown',
  templateUrl: './user-dropdown.component.html',
  styleUrls: ['./user-dropdown.component.css']
})
export class UserDropdownComponent {
isLogged = false;
constructor(private authService: AuthenticationService){
  this.userValue;
}


get userValue(){
  const user = this.authService.userValue
  if(user)
  {
    this.isLogged = true;
    return user;
  }
  this.isLogged = false;
  return null;
}

onClickLogout(){
  this.authService.logout();
  this.isLogged = false;
}
}

