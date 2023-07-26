import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-ingredient-panel',
  templateUrl: './ingredient-panel.component.html',
  styleUrls: ['./ingredient-panel.component.css']
})
export class IngredientPanelComponent {
constructor(public adminService: AdminPanelService){}

}
