using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Clasess
{
    public class Cart
    {
        public int id { get; set; }
        public int idP { get; set; }
        public string article { get; set; }
        public string name { get; set; }
        public string measure_unit { get; set; }
        public float price { get; set; }
        public string manufacturer { get; set; }
        public float discount { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public byte[] img_src { get; set; }
        public int p {get; set;}
    }
}
