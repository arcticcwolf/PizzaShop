import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { PizzasComponent } from './pizzas/pizzas.component';
import { AddPizzaComponent } from './add-pizza/add-pizza.component';
import { ToppingsComponent } from './toppings/toppings.component';
import { IngredientComponent } from './ingredients/ingredients.component';
import { AddToppingComponent } from './add-topping/add-topping.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PizzasComponent,
    AddPizzaComponent,
    ToppingsComponent,
    IngredientComponent,
    AddToppingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'pizzas', component: PizzasComponent },
      { path: 'toppings', component: ToppingsComponent },
      { path: 'add-pizza', component: AddPizzaComponent },
      { path: 'ingredients', component: IngredientComponent },
      { path: 'add-topping', component: AddToppingComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
