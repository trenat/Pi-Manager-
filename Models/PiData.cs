using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PiManagment.Models
{
    public class PiData
    {
        public string IP { set; get; }
        public string Mask { set; get; }
        public string Gate { set; get; }
        public bool UseDHCP { set; get; }
    }
}
