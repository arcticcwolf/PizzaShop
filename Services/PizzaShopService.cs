using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PizzaShopService 
    {
        private readonly PizzaShopRepository repository;

        #region Constructors
        public PizzaShopService()
        {
            repository = new PizzaShopRepository();
        } 
        #endregion

        #region Public Methods
        public int AddPizza(string ingredients, string name, string description)
        {
            return repository.AddPizza(StringToIntArray(ingredients == "-1" ? " " : ingredients), name, description);
        }

        public int AddSingleTopping(string name, string description)
        {
            return repository.AddSingleTopping(name, description);
        }

        public bool DeletePizza(int pizzaId)
        {
            return repository.DeletePizza(pizzaId);
        }

        public Pizza GetPizza(int pizzaId)
        {
            return repository.GetPizza(pizzaId);
        }

        public Pizza[] GetPizzas()
        {
            Pizza[] pizzas = repository.GetPizzas().Result;
            return pizzas;
        }

        public Topping GetTopping(int toppingId)
        {
            return repository.GetTopping(toppingId);
        }

        public Topping[] GetToppings()
        {
            return repository.GetToppings();
        }

        public bool EditPizzaIngredients(int pizzaId, string addingIngredients, string deletingIngredients)
        {
            bool success = true;
            foreach (int add in StringToIntArray(addingIngredients))
            {
                success &= repository.AddTopping(pizzaId, add);
            }
            foreach (int delete in StringToIntArray(deletingIngredients == "-1" ? "" : deletingIngredients))
            {
                success &= repository.DeleteTopping(pizzaId, delete);
            }
            return success;

        }
        public bool DeleteToppings(string removingTopping)
        {
            bool success = true;
            foreach (int delete in StringToIntArray(removingTopping))
            {
                success &= repository.DeleteSingleTopping(delete);
            }
            return success;
        }
        #endregion
        #region Private Methods
        private int[] StringToIntArray(string array)
        {
            List<int> response = new List<int>();
            if (!string.IsNullOrEmpty(array))
                foreach (var item in array.Split(','))
                {
                    int number = 0;
                    response.Add(Int32.TryParse(item.Trim(), out number) ? number : -1);
                }
            return response.ToArray();
        } 
        #endregion
    }
}
