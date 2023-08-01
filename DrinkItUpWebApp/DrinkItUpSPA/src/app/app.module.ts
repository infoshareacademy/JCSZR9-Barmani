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

import { AddonWeatherDirective } from './shared/Directives/addonWeather.directive';
import { AddonShoppingDirective } from './shared/Directives/addonShopping.directive';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MainSearchbarComponent } from './navigation/main-searchbar/main-searchbar.component';
import { DrinkDetailsComponent } from './mainbody/drink-details/drink-details.component';
import { DrinkResolver } from './shared/Services/drinkResolver.service';
import { ResultsSearchComponent } from './mainbody/results-search/results-search.component';
import { JwtInterceptor } from './shared/Interceptors/jwtInterceptor';
import { ErrorInterceptor } from './shared/Interceptors/errorInterceptor';
import { AuthGuardService } from './shared/Services/auth-guard.service';
import { LoginComponent } from './mainbody/login/login.component';
import { UserDropdownComponent } from './navigation/user-dropdown/user-dropdown.component';
import { AdminPanelComponent } from './mainbody/admin-panel/admin-panel.component';
import { DrinkPanelComponent } from './mainbody/admin-panel/drink-panel/drink-panel.component';
import { UnitPanelComponent } from './mainbody/admin-panel/unit-panel/unit-panel.component';
import { IngredientPanelComponent } from './mainbody/admin-panel/ingredient-panel/ingredient-panel.component';
import { MainAlcoholPanelComponent } from './mainbody/admin-panel/main-alcohol-panel/main-alcohol-panel.component';
import { DifficultyPanelComponent } from './mainbody/admin-panel/difficulty-panel/difficulty-panel.component';
import { UserPanelComponent } from './mainbody/admin-panel/user-panel/user-panel.component';
import { RolePanelComponent } from './mainbody/admin-panel/role-panel/role-panel.component';

const appRoutes: Routes = [
{path: '', component: LandingComponent},
{path: 'mixer', component: MixerComponent},
{path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuardService]},
{path: 'registration', component: RegistrationComponent},
{path: 'forgotPassword', component: ForgotPasswordComponent},
{path: 'bycategory', component: ByCategoryComponent},
{path: 'search/:input', component: SearchComponent, },// canActivate: [AuthGuardService]
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
    LoginComponent,
    UserDropdownComponent,
    AdminPanelComponent,
    DrinkPanelComponent,
    UnitPanelComponent,
    IngredientPanelComponent,
    MainAlcoholPanelComponent,
    DifficultyPanelComponent,
    UserPanelComponent,
    RolePanelComponent
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
