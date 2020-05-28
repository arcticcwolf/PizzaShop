using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Services;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Webroot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PizzaShopController : ControllerBase
    {
        private readonly PizzaShopService service;
        public PizzaShopController()
        {
            service = new PizzaShopService();
        }

        [HttpGet]
        [Route("GetPizzas")]
        public IEnumerable<Pizza> GetPizzas()
        {
            return service.GetPizzas();
        }
        [HttpGet]
        [Route("GetPizza/pizzaId/{pizzaId}")]
        public Pizza GetPizza([FromUri]  int pizzaId)
        {
            return service.GetPizza(pizzaId) ;
        }
        [HttpGet]
        [Route("GetToppings")]
        public IEnumerable<Topping> GetToppings()
        {
            return service.GetToppings() ;
        }
        [HttpGet]
        [Route("GetToppings/toppingId/{toppingId}")]
        public Topping GetTopping([FromUri]  int toppingId)
        {
            return service.GetTopping(toppingId);
        }
        [HttpPost]
        [Route("AddSingleTopping/description/{description}/name/{name}")]
        public int AddSingleTopping([FromUri] string name, [FromUri] string description)
        {
            return service.AddSingleTopping(name, description);
        }
        [HttpPost]
        [Route("DeletePizza/pizzaId/{pizzaId}")]
        public bool DeletePizza([FromUri] int pizzaId)
        {
            return service.DeletePizza(pizzaId);
        }
        [HttpPost]
        [Route("AddPizza/name/{name}/description/{description}/ingredients/{ingredients}")]
        public int AddPizza([FromUri] string ingredients, [FromUri] string name, [FromUri] string description)
        {
            return service.AddPizza( ingredients, name, description);
        }
        [HttpPost]
        [Route("EditPizza/pizzaId/{pizzaId}/addingIngredients/{addingIngredients}/deletingIngredients/{deletingIngredients}")]
        public bool EditPizza([FromUri] int pizzaId, [FromUri] string addingIngredients, [FromUri] string deletingIngredients)
        {
            return service.EditPizzaIngredients( pizzaId, addingIngredients, deletingIngredients);
        }
        [HttpPost]
        [Route("DeleteToppings/removingTopping/{removingTopping}")]
        public bool DeleteToppings( [FromUri] string removingTopping)
        {
            return service.DeleteToppings( removingTopping);
        }
    }
}
