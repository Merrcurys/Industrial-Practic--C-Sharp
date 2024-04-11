using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinyl
{
    internal class productModel
    {
        public string Name_Album { get; set; }
        public decimal Price { get; set; }
        public int Musician_ID { get; set; }
        public string Condition_ID_Cover { get; set; }
        public string Condition_ID_Vinyl { get; set; }
        public int Genre_ID { get; set; }

    }
}
