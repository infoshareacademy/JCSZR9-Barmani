import { Component } from '@angular/core';
import { AdminPanelService } from 'src/app/shared/admin-panel.service';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent {
constructor(public adminService: AdminPanelService){}
}
