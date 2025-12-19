using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesi
{
     public class Rezervasyon
    {
        public Guid Id { get; set; }    
        public string MusteriAdi { get; set; }
        public string AracPlaka { get; set; }   
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public double ToplamUcret {  get; set; }    
        public Rezervasyon() { }
        public Rezervasyon(string musteriAdi, string plaka, DateTime baslangic, DateTime bitis, double gunlukFiyat)
        {
            if(bitis<=baslangic)
            {
                throw new Exception("HATA: Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
            }
            Id = Guid.NewGuid();
            MusteriAdi = musteriAdi;
            AracPlaka = plaka;  
            Baslangic = baslangic;  
            Bitis = bitis;

            TimeSpan sure = bitis - baslangic;
            int gun = sure.Days;

            if (sure.TotalDays > gun)
                gun++;
           
            else if (gun == 0)
                gun = 1;

            ToplamUcret = gun * gunlukFiyat;
        }
    }
}
