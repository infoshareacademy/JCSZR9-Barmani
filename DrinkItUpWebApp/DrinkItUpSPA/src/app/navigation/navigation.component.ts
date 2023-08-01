import { Component } from '@angular/core';

import { AddonsService } from '../shared/Services/addons.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
constructor(public addonService: AddonsService)
{

}

}
