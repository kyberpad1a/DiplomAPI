using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomAPI.Models
{
    public class ModelShipping
    {
        public int idshipping { get; set; }
        public string shipping_address { get; set; }
        public int shipping_apartment { get; set; }
        public bool shipping_status { get; set; }
        public bool shipping_taken { get; set; }
        public int order_id { get; set; }
    }
}
