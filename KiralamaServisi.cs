
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace BitirmeProjesi
{
    public class KiralamaServisi
    {
        public List<Arac> Araclar { get; set; } = new List<Arac>();
        public List<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();

        private const string AracDosya = "araclar.json";
        private const string RezervasyonDosya = "rezervasyonlar.json";

        public KiralamaServisi()
        {
            VerileriYukle();
        }

        public void AracEkle(string plaka, string marka, string model, double gunlukFiyat, AracTipi tip)
        {
            foreach (Arac a in Araclar)
            {
                if (a.Plaka == plaka)
                    throw new Exception("Bu plaka zaten sistemde kayıtlı!");
            }

            Arac yeniArac = new Arac(plaka, marka, model, gunlukFiyat, tip);
            Araclar.Add(yeniArac);
            VerileriKaydet();
        }

        public List<string> MusaitAraclariGetir(DateTime baslangic, DateTime bitis)
        {
            List<string> liste = new List<string>();

            foreach (Arac a in Araclar)
            {
                if (AracMusaitMi(a.Plaka, baslangic, bitis))
                {
                    liste.Add(
                        a.Plaka + " - " +
                        a.Marka + " " + a.Model +
                        " (" + a.Tip + ") - " +
                        a.GunlukFiyat + " TL"
                    );
                }
            }
            return liste;
        }

        public bool AracMusaitMi(string plaka, DateTime bas, DateTime bit)
        {
            foreach (Rezervasyon r in Rezervasyonlar)
            {
                if (r.AracPlaka == plaka)
                {
                    if (!(bit <= r.Baslangic || bas >= r.Bitis))
                        return false;
                }
            }
            return true;
        }

        public double AracGunlukFiyatiniGetir(string plaka)
        {
            foreach (Arac a in Araclar)
            {
                if (a.Plaka == plaka)
                    return a.GunlukFiyat;
            }
            throw new Exception("Araç bulunamadı.");
        }



        public void RezervasyonEkle(string musteri, string plaka, DateTime bas, DateTime bit)
        {
            if (!AracMusaitMi(plaka, bas, bit))
                throw new Exception("Araç bu tarihlerde müsait değil.");

            double fiyat = AracGunlukFiyatiniGetir(plaka);
            Rezervasyon r = new Rezervasyon(musteri, plaka, bas, bit, fiyat);

            Rezervasyonlar.Add(r);
            VerileriKaydet();
        }

        public double RezervasyonUcretiHesapla(string plaka, DateTime bas, DateTime bit)
        {
            double fiyat = AracGunlukFiyatiniGetir(plaka);

            TimeSpan sure = bit - bas;
            int gun = sure.Days;
            if (sure.TotalDays > gun)
                gun++;

            if (gun <= 0)
                gun = 1;

            return gun * fiyat;
        }

        public void RezervasyonIptal(string plaka)
        {
            Rezervasyon silinecek = null;

            foreach (Rezervasyon r in Rezervasyonlar)
            {
                if (r.AracPlaka == plaka && r.Baslangic > DateTime.Now)
                {
                    silinecek = r;
                    break;
                }
            }

            if (silinecek == null)
                throw new Exception("İptal edilecek aktif rezervasyon bulunamadı.");

            Rezervasyonlar.Remove(silinecek);
            VerileriKaydet();
        }



        public double ToplamGelir()
        {
            double toplam = 0;
            foreach (Rezervasyon r in Rezervasyonlar)
                toplam += r.ToplamUcret;
            return toplam;
        }

        public List<string> MusteriRezervasyonlariniGetir(string musteri)
        {
            List<string> liste = new List<string>();

            foreach (Rezervasyon r in Rezervasyonlar)
            {
                if (r.MusteriAdi.ToLower() == musteri.ToLower())
                {
                    liste.Add(
                        r.AracPlaka + " | " +
                        r.Baslangic.ToString("dd.MM.yyyy") +
                        " | " + r.ToplamUcret + " TL"
                    );
                }
            }
            return liste;
        }

        public string EnCokKiralananArac()
        {
            if (Rezervasyonlar.Count == 0)
                return "Veri Yok";

            Dictionary<string, int> sayac = new Dictionary<string, int>();

            foreach (Rezervasyon r in Rezervasyonlar)
            {
                if (sayac.ContainsKey(r.AracPlaka))
                    sayac[r.AracPlaka]++;
                else
                    sayac.Add(r.AracPlaka, 1);
            }

            string enCok = "";
            int max = 0;

            foreach (var item in sayac)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    enCok = item.Key;
                }
            }

            return enCok + " (" + max + " işlem)";
        }


        public void VerileriKaydet()
        {
            try
            {
                string aracJson = JsonConvert.SerializeObject(Araclar);
                File.WriteAllText(AracDosya, aracJson);

                string rezJson = JsonConvert.SerializeObject(Rezervasyonlar);
                File.WriteAllText(RezervasyonDosya, rezJson);
            }
            catch
            {
               
            }
        }


        public void VerileriYukle()
        {
            try
            {
                if (File.Exists(AracDosya))
                {
                    string aracJson = File.ReadAllText(AracDosya);
                    Araclar = JsonConvert.DeserializeObject<List<Arac>>(aracJson);
                }

                if (File.Exists(RezervasyonDosya))
                {
                    string rezJson = File.ReadAllText(RezervasyonDosya);
                    Rezervasyonlar = JsonConvert.DeserializeObject<List<Rezervasyon>>(rezJson);
                }
            }
            catch
            {
                Araclar = new List<Arac>();
                Rezervasyonlar = new List<Rezervasyon>();
            }
        }
    }
}
