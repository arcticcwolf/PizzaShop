using PizzaShop.Interfaces;
using PizzaShop.TransferObjects;
using Repository;
using Services;
using System.Web.Http;

namespace Controller
{
    [RoutePrefix("PizzaShop")]
    public class PizzaShopController : ApiController
    {
        private readonly PizzaShopService service;

        //public PizzaShopController(IPizzaShopService service)
        //{
        //    this.service = service;
        //}
        public PizzaShopController()
        {
            service = new PizzaShopService();
        }

        [HttpGet]
        [Route("GetPizzas")]
        public Pizza[] GetPizzas()
        {
            return service.GetPizzas() as Pizza[];
        }
        [HttpGet]
        [Route("GetPizza/pizzaId/{pizzaId}")]
        public Pizza GetPizza(int pizzaId)
        {
            return service.GetPizza(pizzaId) as Pizza;
        }
        [HttpGet]
        [Route("GetToppings")]
        public Topping[] GetToppings()
        {
            return service.GetToppings() as Topping[];
        }
        [HttpGet]
        [Route("GetToppings/toppingId/{toppingId}")]
        public Topping GetTopping(int toppingId)
        {
            return (Topping)service.GetTopping(toppingId) ;
        }
        [HttpPost]
        [Route("AddTopping")]
        public bool AddTopping([FromBody] IngredientDto ingredient)
        {
            return service.AddTopping(ingredient);
        }
        [HttpPost]
        [Route("AddSingleTopping")]
        public int AddSingleTopping([FromBody] string name, string description)
        {
            return service.AddSingleTopping(name,description);
        }

        [HttpPost]
        [Route("DeleteTopping")]
        public bool DeleteTopping([FromBody] IngredientDto ingredient)
        {
            return service.DeleteTopping(ingredient);
        }
        [HttpPost]
        [Route("DeletePizza/pizzaId/{pizzaId}")]
        public bool DeletePizza(int pizzaId)
        {
            return service.DeletePizza(pizzaId);
        }
        //[HttpPost]
        //[Route("AddPizza")]
        //public int AddPizza([FromBody] int[] ingredients, string name, string description)
        //{
        //    return service.AddPizza(ingredients,  name,  description);
        //}
    }
}
