using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PizzaShopRepository 
    {
        #region Constructor
        public PizzaShopRepository() { }
        #endregion

        #region Public Methods
        public int AddPizza(int[] ingredients, string name, string description)
        {
            int modifications = 0;
            Pizza newPizza = new Pizza { name = name, description = description };
            using (var context = new PizzaShopEntities())
            {

                foreach (int toppingId in ingredients)
                {
                    Topping topping = (from o in context.Toppings select o).FirstOrDefault(o => o.id == toppingId);
                    if (topping != null)
                        newPizza.Toppings.Add(topping);
                }
                context.Pizzas.Add(newPizza);
                modifications = context.SaveChanges();
            }
            return modifications > 1 ? newPizza.id : -1;
        }

        public int AddSingleTopping(string name, string description)
        {
            int modifications = 0;
            Topping newTopping = new Topping { name = name, description = description };
            using (var context = new PizzaShopEntities())
            {
                context.Toppings.Add(newTopping);
                modifications = context.SaveChanges();
            }
            return modifications == 1 ? newTopping.id : -1;
        }

        public bool DeleteSingleTopping(int toppingId)
        {
            int modifications = 0;
            using (var context = new PizzaShopEntities())
            {
                Topping topping = context.Toppings.Include(e => e.Pizzas).FirstOrDefault(e => e.id == toppingId);
                if (topping != null)
                {
                    foreach (Pizza p in topping.Pizzas)
                    {
                        p.Toppings.Remove(topping);
                    }
                    context.Toppings.Remove(topping);
                    modifications = context.SaveChanges();
                }
            }
            return modifications >= 1;
        }
        public bool AddTopping(int pizzaId, int toppingId)
        {
            int modifications = 0;
            using (var context = new PizzaShopEntities())
            {
                Pizza pizza = context.Pizzas.Include(e => e.Toppings).FirstOrDefault(e => e.id == pizzaId);
                Topping topping = (from o in context.Toppings select o).FirstOrDefault(o => o.id == toppingId);
                if (pizza != null & topping != null)
                {
                    pizza.Toppings.Add(topping);
                    modifications = context.SaveChanges();
                }
            }
            return modifications >= 1;
        }

        public bool DeletePizza(int pizzaId)
        {
            int modifications = 0;
            using (var context = new PizzaShopEntities())
            {
                Pizza pizza = context.Pizzas.Include(e => e.Toppings).FirstOrDefault(e => e.id == pizzaId);
                if (pizza != null)
                {
                    foreach (Topping t in pizza.Toppings)
                    {
                        t.Pizzas.Remove(pizza);
                    }
                    context.Pizzas.Remove(pizza);
                    modifications = context.SaveChanges();
                }
            }
            return modifications > 1;
        }
        public bool DeleteTopping(int pizzaId, int toppingId)
        {
            int modifications = 0;
            using (var context = new PizzaShopEntities())
            {
                var p1 = context.Pizzas.Include(e => e.Toppings).FirstOrDefault(e => e.id == pizzaId);
                var t1 = (from o in context.Toppings select o).FirstOrDefault(o => o.id == toppingId);
                if (p1 != null & t1 != null)
                {
                    p1.Toppings.Remove(t1);
                    modifications = context.SaveChanges();
                }
            }
            return modifications >= 1;
        }

        public Pizza GetPizza(int pizzaId)
        {
            Pizza pizza = new Pizza();
            try
            {
                using (var context = new PizzaShopEntities())
                {
                    pizza = context.Pizzas.Include(e => e.Toppings).FirstOrDefault(e => e.id == pizzaId);
                    if (pizza != null)
                    {
                        foreach (Topping t in pizza.Toppings)
                        {
                            t.Pizzas = null;
                        }
                    }

                }
            }
            catch (System.InvalidOperationException e)
            {
                return null;
            }
            return pizza;
        }

        public async Task<Pizza[]> GetPizzas()
        {
            using (var context = new PizzaShopEntities())
            {
                return await context.Pizzas.ToArrayAsync();
            }
        }

        public Topping GetTopping(int toppingId)
        {
            Topping topping = new Topping();
            try
            {
                using (var context = new PizzaShopEntities())
                {
                    topping = context.Toppings.First(x => x.id == toppingId);
                }
            }
            catch (System.InvalidOperationException)
            {
                return null;
            }
            return topping;
        }

        public Topping[] GetToppings()
        {
            IList<Topping> toppings = new List<Topping>();
            using (var context = new PizzaShopEntities())
            {
                toppings = context.Toppings.ToList();
            }
            return toppings.ToArray();
        } 
        #endregion
    }
}
