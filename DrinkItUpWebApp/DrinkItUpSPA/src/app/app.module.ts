import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MainbodyComponent } from './mainbody/mainbody.component';
import { AboutusComponent } from './mainbody/aboutus/aboutus.component';
import { MixerComponent } from './mainbody/mixer/mixer.component';
import { SearchComponent } from './search/search.component';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterbarComponent } from './footerbar/footerbar.component';
import { DropdownDirective } from './shared/dropdown.directive';

@NgModule({
  declarations: [
    AppComponent,
    MainbodyComponent,
    AboutusComponent,
    MixerComponent,
    SearchComponent,
    NavigationComponent,
    FooterbarComponent,
    DropdownDirective
  ],
  imports: [
    BrowserModule, HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
