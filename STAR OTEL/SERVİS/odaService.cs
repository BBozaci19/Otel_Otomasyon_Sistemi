using Otel_Star.DAL;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;
using static Otel_Star.DAL.odaDAL;

namespace Otel_Star.SERVICES
{
    public class OdaService
    {
        private readonly OdaDAL _odaDAL;

        public OdaService()
        {
            _odaDAL = new OdaDAL();
        }

        public List<Oda> OdalariGetir()
        {
            return _odaDAL.GetirOdalar();
        }

        public void OdaEkle(Oda oda)
        {
            if (string.IsNullOrEmpty(oda.oda_numara))
            {
                throw new Exception("Oda numarası boş olamaz.");
            }

            _odaDAL.EkleOda(oda);
        }

        public void OdaSil(int odaId)
        {
            _odaDAL.SilOda(odaId);
        }
    }
}
