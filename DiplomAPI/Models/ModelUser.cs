using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomAPI.Models
{
    public class ModelUser
    {
        public int iduser { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool active { get; set; }
    }
}
