import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-role-panel',
  templateUrl: './role-panel.component.html',
  styleUrls: ['./role-panel.component.css']
})
export class RolePanelComponent {
constructor(public adminService: AdminPanelService){}
}
