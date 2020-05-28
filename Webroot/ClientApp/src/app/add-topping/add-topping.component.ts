import { Component,  Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'add-topping',
  templateUrl: './add-topping.html'
})
export class AddToppingComponent {
  public name: string = "";
  public description: string = "";
  public http: HttpClient;
  public validations: string[] = [];
  public router: Router;
  public baseUrl: string;
  private sub: any;
  public constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    router: Router
  ) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.router = router;
  }
  save() {
    if (this.name === "") { this.validations.push("Name cannot be empty"); return; }
    else {
      const url = this.baseUrl + 'pizzashop/addsingletopping/description/' + this.description + "/name/" + this.name; 
      this.http.post<number>(url, null).subscribe(result => {
        console.error(result);
        if (result > 0) {
          this.router.navigate(['/','toppings']);
        }
      }, error => console.error(error));
    }
  }
  clear() {
    this.name = "";
    this.description = "";
  }

}
