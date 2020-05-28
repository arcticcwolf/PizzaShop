using System;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;

namespace UnitTest
{
    [TestClass]
    public class PizzaShopServiceTest
    {
        private PizzaShopService service;
        public PizzaShopServiceTest()
        {
            service = new PizzaShopService();
        }

        [TestMethod]
        public void CreatePizzaWithTopping_test()
        {
            int topping = AddSingleTopping_test();
            AddPizza_test(new int[] { topping }, "Vegy", "Vegetarian Pizza");
            GetPizzas_test();
        }

        [TestMethod]
        public void CreatePizzaThenDeleteTopping_test()
        {
            int toppingId = AddSingleTopping_test();
            int pizzaId = AddPizza_test(new int[] { toppingId }, "Criolla", "Bolivian Pizza");
            DeleteTopping_test(toppingId, pizzaId);
        }
        [TestMethod]
        public void CreateThenLoad_test()
        {
            int toppingId = AddSingleTopping_test();
            int pizzaId = AddPizza_test(new int[] { toppingId }, "Vegy2", "Vegetarian Pizza2");
            GetPizza_test(pizzaId);

        }
        [TestMethod]
        public void CreatePizzaThenAddTopping_test()
        {
            int toppingId = AddSingleTopping_test();
            int toppingId2 = AddSingleTopping_test();
            int pizzaId = AddPizza_test(new int[] { toppingId }, "Hawa", "Fruits Pizza");
        }

        public void GetPizzas_test()
        {
            Assert.IsTrue(service.GetPizzas().Length >= 0);
        }

        public void GetPizza_test(int pizzaId)
        {
            Assert.IsNotNull(service.GetPizza(pizzaId), "Object Not Found");
        }

        public void GetToppings_test()
        {
            Assert.IsTrue(service.GetToppings().Length >= 0);
        }

        public void GetTopping_test(int toppingId)
        {
            Assert.IsNotNull(service.GetTopping(toppingId), "Object Not Found");
        }

        public int AddSingleTopping_test()
        {
            int result = service.AddSingleTopping("Onion", "Onion is healthly");
            Assert.IsTrue(result >= 0);
            return result;
        }

        public void DeleteTopping_test(int toppingId, int pizzaId)
        {
            Assert.IsTrue(service.DeleteToppings( toppingId.ToString()));
        }

        public void DeletePizza_test(int pizzaId)
        {
            Assert.IsTrue(service.DeletePizza(pizzaId));
        }

        public int AddPizza_test(int[] ingredients, string name, string description)
        {
            int result = service.AddPizza( string.Join(",", ingredients.Select(n=> n.ToString()).ToArray()), name, description);
            Assert.IsTrue(result >= 0);
            return result;
        }
    }
}
