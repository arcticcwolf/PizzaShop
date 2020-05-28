using PizzaShop.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.Interfaces
{
    public interface IPizzaShopRespository
    {
        EntityBase[] GetPizzas();
        EntityBase GetPizza(int pizzaId);
        EntityBase[] GetToppings();
        EntityBase GetTopping(int toppingId);
        bool AddTopping(int pizza, int topping);
        bool DeleteTopping(int pizzaId, int toppingId);
        bool DeletePizza(int pizzaId);
        int AddPizza(int[] ingredients, string name, string description);
        int AddSingleTopping(string name, string description);
    }
}
