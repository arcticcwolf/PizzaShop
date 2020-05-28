using PizzaShop.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Interfaces
{
    public interface IPizzaShopService
    {
       EntityBase[] GetPizzas();
       EntityBase GetPizza(int pizzaId);
       EntityBase[] GetToppings();
       EntityBase GetTopping(int toppingId);
        bool AddTopping(IngredientDto ingredient);
        bool DeleteTopping(IngredientDto ingredient);
        bool DeletePizza(int pizzaId);
        int AddPizza(int[] ingredients, string name, string description);
        int AddSingleTopping(string name, string description);
    }
}
