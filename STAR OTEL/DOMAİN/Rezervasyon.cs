using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel_Star.DOMAİN
{
    public class Rezervasyon
    {
        public int rezervasyon_id { get; set; }
        public int musteri_id { get; set; }
        public int oda_id { get; set; }
        public DateTime rezervasyon_tarihi { get; set; }
        public decimal rezervasyon_tutari { get; set; }
    }
}
