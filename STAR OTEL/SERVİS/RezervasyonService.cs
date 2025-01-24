using Otel_Star.DAL;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;

namespace Otel_Star.SERVICES
{
    public class RezervasyonService
    {
        private readonly RezervasyonDAL _rezervasyonDAL;

        public RezervasyonService()
        {
            _rezervasyonDAL = new RezervasyonDAL();
        }

        public List<Rezervasyon> RezervasyonlariGetir()
        {
            return _rezervasyonDAL.GetirRezervasyonlar();
        }

        public void RezervasyonEkle(Rezervasyon rezervasyon)
        {
            if (rezervasyon.musteri_id <= 0 || rezervasyon.oda_id <= 0)
            {
                throw new Exception("Müşteri ID ve Oda ID geçerli olmalıdır.");
            }

            if (rezervasyon.rezervasyon_tutari <= 0)
            {
                throw new Exception("Rezervasyon tutarı sıfırdan büyük olmalıdır.");
            }

            _rezervasyonDAL.EkleRezervasyon(rezervasyon);
        }

        public void RezervasyonSil(int rezervasyonId)
        {
            _rezervasyonDAL.SilRezervasyon(rezervasyonId);
        }
    }
}
