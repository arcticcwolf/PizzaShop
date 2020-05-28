import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Topping } from '../model/pizzashop.model';
import * as clone from 'clone';

@Component({
  selector: 'add-pizza',
  templateUrl: './add-pizza.html'
})
export class AddPizzaComponent implements OnInit {
  public toppings: Topping[];
  public originalToppings: Topping[];
  public ingredients: number[] = [];
  public name: string = "";
  public arrayPos: number[] = [];
  public description: string= "";
  public selectingTopping: number;
  public baseUrl: string;
  public http: HttpClient;
  public validations: string[] = [];
  public router: Router;

  public constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    router : Router
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.router = router;
  }
  ngOnInit() {
    this.getToppings();
  }
  getToppings() {
    this.http.get<Topping[]>(this.baseUrl + 'pizzashop/gettoppings').
      subscribe(result => {
        this.originalToppings = result;
        this.toppings = clone(this.originalToppings);
      },
        error => console.error(error));
  }

  addIngredient() {
    const found = this.ingredients.find(element => element == this.selectingTopping);
    if (!found) {
      this.ingredients.push(this.selectingTopping);
      this.arrayPos.push(this.toppings.findIndex(x => x.id == this.selectingTopping));
      this.validations = [];
    }
  }
  save() {
    this.validations = [];
    if (this.name === "") this.validations.push("Name cannot be empty");
    if (this.ingredients.length < 1) this.validations.push("At least select one topping");
    if (this.validations.length > 0) {
      return;
    }
    else {
      const url = this.baseUrl + 'pizzashop/addpizza/name/' + this.name + "/description/" + this.description + "/ingredients/" + this.ingredients.join().split(', ');
      this.http.post<number>(url, null).subscribe(result => {
        console.error(result);
        if (result > 0) {
          this.router.navigate(['/', 'pizzas']);
        }
      }, error => { console.error(error); this.toppings = clone(this.originalToppings); });
    }
    
  }

  clear() {
    this.ingredients = [];
    this.name = "";
    this.description = "";
    this.validations = [];
    this.arrayPos = [];
  }

}
