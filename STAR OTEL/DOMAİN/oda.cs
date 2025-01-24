using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel_Star.DOMAİN
{
    public class Oda
    {
        public int oda_id { get; set; }
        public string oda_numara { get; set; }
        public string oda_tur { get; set; }
        public decimal oda_fiyat { get; set; }

        public Oda() { }

        public Oda(int oda_id, string oda_numara, string oda_tur, decimal oda_fiyat)
        {
            this.oda_id = oda_id;
            this.oda_numara = oda_numara;
            this.oda_tur = oda_tur;
            this.oda_fiyat = oda_fiyat;
        }
    }
}
