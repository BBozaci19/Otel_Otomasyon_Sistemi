using Otel_Star.DAL;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;

namespace Otel_Star.SERVICES
{
    public class MusteriService
    {
        private readonly MusteriDAL _musteriDAL;

        public MusteriService()
        {
            _musteriDAL = new MusteriDAL();
        }

        public List<Musteri> MusterileriGetir()
        {
            return _musteriDAL.GetirMusteriler();
        }

        public void MusteriEkle(Musteri musteri)
        {
            if (string.IsNullOrEmpty(musteri.musteri_adi) || string.IsNullOrEmpty(musteri.musteri_soyadi))
            {
                throw new Exception("Müşteri adı ve soyadı boş olamaz.");
            }

            _musteriDAL.EkleMusteri(musteri);
        }

        public void MusteriSil(int musteriId)
        {
            _musteriDAL.SilMusteri(musteriId);
        }
    }
}
