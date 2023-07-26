import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-drink-panel',
  templateUrl: './drink-panel.component.html',
  styleUrls: ['./drink-panel.component.css']
})
export class DrinkPanelComponent {
constructor(public adminService: AdminPanelService){}
}
