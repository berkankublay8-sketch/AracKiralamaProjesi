using System;

namespace BitirmeProjesi
{
    internal class Program
    {
        static KiralamaServisi servis = new KiralamaServisi();

        static void Main(string[] args)
        {
            if (servis.Araclar.Count == 0)
            {
                try
                {
                    servis.AracEkle("34ABC01", "BMW", "320i", 1500, AracTipi.Sedan);
                    servis.AracEkle("06XYZ99", "Audi", "Q7", 2500, AracTipi.SUV);
                }
                catch { }
            }

            int secim = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("===  AKILLI ARAÇ KİRALAMA SİSTEMİ  ===");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Yeni Araç Ekle");
                Console.WriteLine("2. Müsait Araçları Listele");
                Console.WriteLine("3. Rezervasyon Yap");
                Console.WriteLine("4. Rezervasyon İptal Et");
                Console.WriteLine("5. Raporlar");
                Console.WriteLine("0. Çıkış");
                Console.Write("Seçiminiz: ");

                if (!int.TryParse(Console.ReadLine(), out secim))
                {
                    Console.WriteLine("Hatalı giriş!");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (secim)
                    {
                        case 1:
                            Console.Write("Plaka: ");
                            string p = Console.ReadLine().ToUpper();
                            Console.Write("Marka: ");
                            string m = Console.ReadLine();
                            Console.Write("Model: ");
                            string mo = Console.ReadLine();

                            Console.Write("Günlük Fiyat: ");
                            double f;
                            if (!double.TryParse(Console.ReadLine(), out f))
                                throw new Exception("Fiyat hatalı!");

                            Console.WriteLine("Araç Tipi (0: Sedan, 1: SUV, 2: Hatchback, 3: Ticari): ");
                            int t;
                            if (!int.TryParse(Console.ReadLine(), out t) || t < 0 || t > 3)
                                throw new Exception("Araç tipi hatalı!");

                            servis.AracEkle(p, m, mo, f, (AracTipi)t);
                            Console.WriteLine("Araç başarıyla eklendi.");
                            break;

                        case 2:
                            Console.Write("Başlangıç Tarihi: ");
                            DateTime b1 = DateTime.Parse(Console.ReadLine());
                            Console.Write("Bitiş Tarihi: ");
                            DateTime b2 = DateTime.Parse(Console.ReadLine());

                            var liste = servis.MusaitAraclariGetir(b1, b2);
                            if (liste.Count == 0)
                                Console.WriteLine("Uygun araç yok.");
                            else
                                foreach (string item in liste)
                                    Console.WriteLine(item);
                            break;

                        case 3:
                            Console.Write("Müşteri Adı: ");
                            string mus = Console.ReadLine();
                            Console.Write("Plaka: ");
                            string plk = Console.ReadLine().ToUpper();
                            Console.Write("Başlangıç: ");
                            DateTime s = DateTime.Parse(Console.ReadLine());
                            Console.Write("Bitiş: ");
                            DateTime e = DateTime.Parse(Console.ReadLine());

                            double ucret = servis.RezervasyonUcretiHesapla(plk, s, e);
                            Console.WriteLine("Tahmini Tutar: " + ucret + " TL");
                            Console.Write("Onay (E/H): ");

                            if (Console.ReadLine().ToUpper() == "E")
                            {
                                servis.RezervasyonEkle(mus, plk, s, e);
                                Console.WriteLine("Rezervasyon yapıldı.");
                            }
                            break;

                        case 4:
                            Console.Write("İptal edilecek plaka: ");
                            servis.RezervasyonIptal(Console.ReadLine().ToUpper());
                            Console.WriteLine("Rezervasyon iptal edildi.");
                            break;

                        case 5:
                            Console.WriteLine("Toplam Ciro: " + servis.ToplamGelir() + " TL");
                            Console.WriteLine("En Popüler Araç: " + servis.EnCokKiralananArac());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("HATA: " + ex.Message);
                }

                if (secim != 0)
                {
                    Console.WriteLine("Devam etmek için tuşa bas.");
                    Console.ReadKey();
                }

            } while (secim != 0);
        }
    }
}

