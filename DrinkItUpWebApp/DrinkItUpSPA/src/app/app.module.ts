import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { FooterbarComponent } from './footerbar/footerbar.component';

import { AboutusComponent } from './mainbody/aboutus/aboutus.component';
import { MixerComponent } from './mainbody/mixer/mixer.component';
import { SearchComponent } from './mainbody/search/search.component';
import { MainbodyComponent } from './mainbody/mainbody.component';
import { RegistrationComponent } from './mainbody/registration/registration.component';
import { WeatherComponent } from './mainbody/addons/weather/weather.component';
import { ShoppinglistComponent } from './mainbody/addons/shoppinglist/shoppinglist.component';
import { AddonsComponent } from './mainbody/addons/addons.component';
import { LandingComponent } from './mainbody/landing/landing.component';
import { ByCategoryComponent } from './mainbody/by-category/by-category.component';
import { StatisticsComponent } from './mainbody/statistics/statistics.component';

import { NavigationComponent } from './navigation/navigation.component';
import { AuthorizationComponent } from './navigation/authorization/authorization.component';

import { AddonWeatherDirective } from './shared/addonWeather.directive';
import { AddonShoppingDirective } from './shared/addonShopping.directive';

const appRoutes: Routes = [
{path: '', component: LandingComponent},
{path: 'mixer', component: MixerComponent},
{path: 'registration', component: RegistrationComponent},
{path: 'bycategory', component: ByCategoryComponent},
{path: 'search', component: SearchComponent},
{path: 'aboutus', component: AboutusComponent}
];

@NgModule({
  declarations: [
    AppComponent,
    MainbodyComponent,
    AboutusComponent,
    MixerComponent,
    SearchComponent,
    NavigationComponent,
    FooterbarComponent,
    AddonWeatherDirective,
    AddonShoppingDirective,
    RegistrationComponent,
    AuthorizationComponent,
    WeatherComponent,
    ShoppinglistComponent,
    AddonsComponent,
    LandingComponent,
    ByCategoryComponent,
    StatisticsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
