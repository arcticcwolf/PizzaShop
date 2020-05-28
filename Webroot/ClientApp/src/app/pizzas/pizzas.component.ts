import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pizza } from '../model/pizzashop.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pizzas',
  templateUrl: './pizzas.html'
})
export class PizzasComponent {
  public pizzas: Pizza[];
  public router: Router;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, router: Router) {
    this.router = router;
    http.get<Pizza[]>(baseUrl + 'pizzashop/getpizzas').
      subscribe(result => {
        this.pizzas = result;
      },
        error => console.error(error));
  }

  viewIngredients(view) {
    this.router.navigate(['/', 'ingredients'], { queryParams: {id : view }});
  }
}

