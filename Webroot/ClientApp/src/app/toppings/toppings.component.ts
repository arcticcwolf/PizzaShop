import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Topping } from '../model/pizzashop.model';
import { Router } from '@angular/router';
import * as clone from 'clone';

@Component({
  selector: 'app-toppings',
  templateUrl: './toppings.html'
})
export class ToppingsComponent implements OnInit {
  public toppings: Topping[];
  public originalToppings: Topping[];
  public newTopping: Topping;
  public http: HttpClient;
  public selectingTopping: number;
  public router: Router;
  public baseUrl: string;
  public removingToppings: number[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.router = router;
  }

  ngOnInit() {
    this.http.get<Topping[]>(this.baseUrl + 'pizzashop/gettoppings').
      subscribe(result => {
        this.originalToppings = result;
        this.toppings = clone(this.originalToppings);
      },
        error => console.error(error));
  }
  
  Save() {
    if (this.removingToppings.length > 1) {
      const url = this.baseUrl + 'pizzashop/deleteToppings/removingTopping/' + this.removingToppings.join().split(', ');
      this.http.post<boolean>(url, null).subscribe(result => {
        console.error(result);
        if (result == true) {
          this.router.navigate(['/', 'toppings']);
        }
      }, error => { console.error(error); this.toppings = clone(this.originalToppings); });
    }
  }

  Clear() {
    this.removingToppings = [];
    this.toppings = clone(this.originalToppings);
    this.router.navigate(['/', 'toppings']);
  }

  Add() {
    this.router.navigate(['/', 'add-topping']);
  }

  remove(removeId) {
    const found = this.removingToppings.find(element => element == removeId);
    if (!found) {
      this.removingToppings.push(removeId);
      const index = this.toppings.map(function (e) { return e.id }).indexOf(removeId);
      this.toppings.splice(index, 1);
    }
  }

}
