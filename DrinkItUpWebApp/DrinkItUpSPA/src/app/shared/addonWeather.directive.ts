import { Directive, HostBinding, HostListener } from "@angular/core";
import { AddonsService } from "./addons.service";

@Directive({
    selector: '[appAddonWeather]'
})
export class AddonWeatherDirective{
isOpen = false;

constructor(private addonsService: AddonsService){}

@HostListener('click') toggleOpen() {
    this.isOpen = !this.isOpen;
    this.addonsService.activateWeather.next(this.isOpen);
}

}