using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PizzaShopRepositoryTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException), "Entity already saved")]
        public void test_Repository_savePizza()
        {
            Pizza pizza = new Pizza { id = 1, description = "delicious pizza", name = "Irish" };
            using (var context = new PizzaShopEntities())
            {
                context.Pizzas.Add(pizza);
                context.SaveChanges();
            }
        }
        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException), "Entity already saved")]
        public void test_Repository_saveTopping()
        {
            Topping topping = new Topping { id = 1, description = "ham is good", name = "Ham" };
            using (var context = new PizzaShopEntities())
            {
                context.Toppings.Add(topping);
                context.SaveChanges();
            }
        }
    }
}
