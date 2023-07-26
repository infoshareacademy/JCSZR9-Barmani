import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/admin-panel.service';

@Component({
  selector: 'app-main-alcohol-panel',
  templateUrl: './main-alcohol-panel.component.html',
  styleUrls: ['./main-alcohol-panel.component.css']
})
export class MainAlcoholPanelComponent {
constructor(public adminService: AdminPanelService){}
}
