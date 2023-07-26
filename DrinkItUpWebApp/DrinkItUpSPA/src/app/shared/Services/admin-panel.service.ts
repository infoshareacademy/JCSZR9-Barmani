import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminPanelService {
  categoryChoose$ = new Subject<string>();
  constructor() { }

  public onCategoryChoose(input: string){
    this.categoryChoose$.next(input);
  }
}
