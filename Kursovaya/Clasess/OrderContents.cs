using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya.Clasess
{
    public class OrderContents
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public string article { get; set; }
        public int count { get; set; }

    }
}
