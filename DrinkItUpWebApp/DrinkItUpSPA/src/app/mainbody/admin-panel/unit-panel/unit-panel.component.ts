import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/admin-panel.service';

@Component({
  selector: 'app-unit-panel',
  templateUrl: './unit-panel.component.html',
  styleUrls: ['./unit-panel.component.css']
})
export class UnitPanelComponent {
constructor(public adminService: AdminPanelService){}

}
