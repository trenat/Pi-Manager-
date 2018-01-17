using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiManagment.Models
{
    public class Pair
    {
        public int ID { set; get; }
        public string Value { set; get; }
        public Pair() { }
        public Pair(int id, string value)
        {
            this.ID = id;
            this.Value = value;
        }
    }
}
