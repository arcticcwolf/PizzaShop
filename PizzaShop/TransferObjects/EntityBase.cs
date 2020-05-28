using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop.TransferObjects
{
    public class EntityBase
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual EntityBase[] children { get; set; }
    }
}
