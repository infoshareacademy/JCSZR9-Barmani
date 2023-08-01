import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil, tap } from 'rxjs';
import { AdminPanelService } from 'src/app/shared/Services/admin-panel.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnDestroy {
activePanel = 'drink';
destroyed$ = new Subject();

constructor(private adminService: AdminPanelService){
  this.adminService.categoryChoose$.pipe(
    tap( data => this.activePanel = data),
    takeUntil(this.destroyed$)
  )
  .subscribe();
}
  ngOnDestroy(): void {
    this.destroyed$.complete();
  }
}
