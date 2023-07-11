import { Directive, HostBinding, HostListener } from "@angular/core";
import { AddonsService } from "./addons.service";

@Directive({
    selector: '[appAddonShopping]'
})
export class AddonShoppingDirective{
isOpen = false;

constructor(private addonsService: AddonsService){}

@HostListener('click') toggleOpen() {
    this.isOpen = !this.isOpen;
    this.addonsService.activateShopping.next(this.isOpen);
}

}