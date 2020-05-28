export interface  Pizza {
  id: number;
  name: string;
  description: string;
  toppings: Topping[];
}

export interface Topping {
  id: number;
  name: string;
  description: string;
  pizzas: Pizza[];
}

