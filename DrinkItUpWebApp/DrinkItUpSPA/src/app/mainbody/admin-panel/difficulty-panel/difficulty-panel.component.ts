import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-difficulty-panel',
  templateUrl: './difficulty-panel.component.html',
  styleUrls: ['./difficulty-panel.component.css']
})
export class DifficultyPanelComponent {
constructor(public adminService: AdminPanelService){}
}
