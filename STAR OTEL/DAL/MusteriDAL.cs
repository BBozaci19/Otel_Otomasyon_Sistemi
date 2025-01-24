using MySql.Data.MySqlClient;
using Otel_Star.DOMAİN;
using System;
using System.Collections.Generic;

namespace Otel_Star.DAL
{
    internal class MusteriDAL
    {
        private readonly Baglanti _baglanti;

        public MusteriDAL()
        {
            _baglanti = new Baglanti();
        }

        public List<Musteri> GetirMusteriler()
        {
            List<Musteri> musteriler = new List<Musteri>();
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "SELECT musteri_id, musteri_adi, musteri_soyadi, musteri_telefon, musteri_tc FROM musteri";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            musteriler.Add(new Musteri
                            {
                                musteri_id = reader.GetInt32("musteri_id"),
                                musteri_adi = reader.GetString("musteri_adi"),
                                musteri_soyadi = reader.GetString("musteri_soyadi"),
                                musteri_telefon = reader.GetString("musteri_telefon"),
                                musteri_tc = reader.GetString("musteri_tc")
                            });
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Veritabanı hatası: {ex.Message}");
            }
            return musteriler;
        }

        public void EkleMusteri(Musteri musteri)
        {
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "INSERT INTO musteri (musteri_adi, musteri_soyadi, musteri_telefon, musteri_tc) VALUES (@adi, @soyadi, @telefon, @tc)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@adi", musteri.musteri_adi);
                    cmd.Parameters.AddWithValue("@soyadi", musteri.musteri_soyadi);
                    cmd.Parameters.AddWithValue("@telefon", musteri.musteri_telefon);
                    cmd.Parameters.AddWithValue("@tc", musteri.musteri_tc);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Veritabanına müşteri eklerken bir hata oluştu: {ex.Message}");
            }
        }

        public void SilMusteri(int musteriId)
        {
            try
            {
                using (var conn = _baglanti.baglantiGetir())
                {
                    var query = "DELETE FROM musteri WHERE musteri_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", musteriId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Müşteri silinirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
