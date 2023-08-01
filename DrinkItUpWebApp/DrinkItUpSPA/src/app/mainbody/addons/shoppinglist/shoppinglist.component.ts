import { Component } from '@angular/core';
import { AddonsService } from 'src/app/shared/Services/addons.service';

@Component({
  selector: 'app-shoppinglist',
  templateUrl: './shoppinglist.component.html',
  styleUrls: ['./shoppinglist.component.css']
})
export class ShoppinglistComponent {
constructor(public addonService: AddonsService){}
}
