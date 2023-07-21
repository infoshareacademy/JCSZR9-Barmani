import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
import { ForgotPasswordComponent } from './navigation/forgot-password/forgot-password.component';

import { AddonWeatherDirective } from './shared/addonWeather.directive';
import { AddonShoppingDirective } from './shared/addonShopping.directive';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MainSearchbarComponent } from './navigation/main-searchbar/main-searchbar.component';
import { DrinkDetailsComponent } from './mainbody/drink-details/drink-details.component';
import { DrinkResolver } from './shared/drinkResolver.service';
import { ResultsSearchComponent } from './mainbody/results-search/results-search.component';
import { JwtInterceptor } from './shared/jwtInterceptor';
import { ErrorInterceptor } from './shared/errorInterceptor';
import { AuthGuardService } from './shared/auth-guard.service';
import { LoginComponent } from './mainbody/login/login.component';

const appRoutes: Routes = [
{path: '', component: LandingComponent},
{path: 'mixer', component: MixerComponent},
{path: 'registration', component: RegistrationComponent},
{path: 'forgotPassword', component: ForgotPasswordComponent},
{path: 'bycategory', component: ByCategoryComponent},
{path: 'search/:input', component: SearchComponent, canActivate: [AuthGuardService]},
{path: 'aboutus', component: AboutusComponent},
{path: 'detail/:id', component: DrinkDetailsComponent, resolve: {drink: DrinkResolver} },
{path: 'login', component: LoginComponent },
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
    StatisticsComponent,
    ForgotPasswordComponent,
    MainSearchbarComponent,
    DrinkDetailsComponent,
    ResultsSearchComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, RouterModule.forRoot(appRoutes),FormsModule,CommonModule
  ],
  providers: [DrinkResolver,
  {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi:true},
  {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi:true}
],
  bootstrap: [AppComponent]
})
export class AppModule { }
