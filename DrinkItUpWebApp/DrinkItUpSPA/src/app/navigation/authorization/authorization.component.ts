import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { AuthenticationService } from 'src/app/shared/authentication.service';

@Component({
  selector: 'app-authorization',
  templateUrl: './authorization.component.html',
  styleUrls: ['./authorization.component.css']
})
export class AuthorizationComponent {
email: string = '';
password: string = '';

constructor(private authService: AuthenticationService){}
onSubmit(){
  this.authService.login(this.email, this.password).subscribe(data =>{});
  
}
}
