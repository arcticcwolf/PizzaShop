import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Topping, Pizza } from '../model/pizzashop.model';
import * as clone from 'clone';

@Component({
  selector: 'ingredient-mant',
  templateUrl: './ingredients.html'
})

export class IngredientComponent  implements OnInit {
  public pizza: Pizza;
  public originalPizza: Pizza;
  public toppings: Topping[];
  public removingIngredients: number[] = [];
  public addingIngredients: number[] = [];
  public baseUrl: string;
  public http: HttpClient;
  public selectingTopping: number;
  public router: Router;
  public route: ActivatedRoute;
  public pizzaId: number;
  public sub: any;

  public constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    router: Router,
    route: ActivatedRoute
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.router = router;
    this.route = route;
  }
  ngOnInit() {
    this.sub = this.route
      .queryParams
      .subscribe(params => {
        this.pizzaId = +params['id'] ;
      });
      if (this.pizzaId >= 0) {
        const url = this.baseUrl + 'pizzashop/getpizza/pizzaid/' + this.pizzaId;
        this.http.get<Pizza>(url).
            subscribe(result => {
              this.originalPizza = result;
              this.pizza = clone(this.originalPizza);
            },
              error => console.error(error));

        this.http.get<Topping[]>(this. baseUrl + 'pizzashop/gettoppings').
          subscribe(result => {
            this.toppings = result;
          },
            error => console.error(error));
        }
  }

  addIngredient() {
    const found = this.pizza.toppings.find(element => element.id == this.selectingTopping);
    if (!found) {
      const newTopping = this.toppings.find(t => t.id == this.selectingTopping);
      if (newTopping) {
        this.addingIngredients.push(newTopping.id);
        this.pizza.toppings.push(newTopping);
      }
    }
    this.selectingTopping = 0;
  }
  removeIngredient(removeId) {
    const found = this.removingIngredients.find(element => element == removeId);
    if (!found) {
      this.removingIngredients.push(removeId);
      const index = this.pizza.toppings.map(function (e) { return e.id }).indexOf(removeId);
      this.pizza.toppings.splice(index, 1);
    }
  }

  clear() {
    this.removingIngredients = [];
    this.addingIngredients = [];
    this.pizza = null;
    this.pizza = this.originalPizza;
    this.router.navigate(['/', 'ingredients'], { queryParams: { id: this.pizzaId } });
  }

  save() {
    const adding = this.addingIngredients.length == 0 ? " " : this.addingIngredients.join().split(', ');
    const removing = this.removingIngredients.length == 0 ? "-1" : this.removingIngredients.join().split(', ');
    const url = this.baseUrl + 'pizzashop/editpizza/pizzaId/' + this.pizzaId + "/addingIngredients/" + adding + "/deletingIngredients/" + removing;
    this.http.post<boolean>(url, this.pizzaId).subscribe(result => {
      if (result == true) {
        this.router.navigate(['/', 'pizzas']);
      }
    }, error => { console.error(error); this.pizza = clone(this.originalPizza); });
  }
}
