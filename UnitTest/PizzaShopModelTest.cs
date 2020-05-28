using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PizzaShopModelTest
    {
        [TestMethod]
        public void Test_Model()
        {
            Pizza pizza = new Pizza { id = 1, description = "delicious pizza", name = "Irish" };
            Topping peperoni = new Topping { id = 1, name = "Peperoni", description = "delicious ingredent" };
            Topping ham = new Topping { id = 2, name = "Ham", description = "delicious ingredent for pizza" };
            IList<Topping> ingredients = new List<Topping>();
            ingredients.Add(peperoni);
            ingredients.Add(ham);
            pizza.Toppings = ingredients;
            Assert.AreSame("delicious pizza", pizza.description);
            Assert.IsTrue(pizza.Toppings.Any(z => z.name == "Peperoni"));
            Assert.IsFalse(peperoni.Pizzas.Any(x => x.name == "Simple"));
        }
    }
}
