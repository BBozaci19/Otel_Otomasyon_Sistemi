using MySql.Data.MySqlClient;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;

namespace Otel_Star.DAL
{
    internal class RezervasyonDAL
    {
        private readonly Baglanti _baglanti;

        public RezervasyonDAL()
        {
            _baglanti = new Baglanti();
        }

        public List<Rezervasyon> GetirRezervasyonlar()
        {
            List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "SELECT rezervasyon_id, musteri_id, oda_id, rezervasyon_tarihi, rezervasyon_tutari FROM rezervasyon";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rezervasyonlar.Add(new Rezervasyon
                            {
                                rezervasyon_id = reader.GetInt32("rezervasyon_id"),
                                musteri_id = reader.GetInt32("musteri_id"),
                                oda_id = reader.GetInt32("oda_id"),
                                rezervasyon_tarihi = reader.GetDateTime("rezervasyon_tarihi"),
                                rezervasyon_tutari = reader.GetDecimal("rezervasyon_tutari")
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            }
            return rezervasyonlar;
        }

        public void EkleRezervasyon(Rezervasyon rezervasyon)
        {
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "INSERT INTO rezervasyon (musteri_id, oda_id, rezervasyon_tarihi, rezervasyon_tutari) VALUES (@musteri_id, @oda_id, @tarih, @tutar)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@musteri_id", rezervasyon.musteri_id);
                    cmd.Parameters.AddWithValue("@oda_id", rezervasyon.oda_id);
                    cmd.Parameters.AddWithValue("@tarih", rezervasyon.rezervasyon_tarihi);
                    cmd.Parameters.AddWithValue("@tutar", rezervasyon.rezervasyon_tutari);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Veritabanına rezervasyon eklerken bir hata oluştu: {ex.Message}");
            }
        }

        public void SilRezervasyon(int rezervasyonId)
        {
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "DELETE FROM rezervasyon WHERE rezervasyon_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", rezervasyonId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Rezervasyon silinirken bir hata oluştu: {ex.Message}");
                
            }
        }
    }
}
