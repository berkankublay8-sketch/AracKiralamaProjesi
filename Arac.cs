using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesi
    {
        public enum AracTipi
        {
            Sedan,
            SUV,
            Hatchback,
            Ticari
        }
    public class Arac
    {
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public double GunlukFiyat { get; set; }
        public AracTipi Tip { get; set; }

        public Arac() { }   

        public Arac(string plaka,string marka,string model,double gunlukFiyat,AracTipi tip) 
        { 
         if(plaka == "")
            {
                throw new Exception("Plaka boş olamaz");
            }
         if(gunlukFiyat<=0)
            {
                throw  new Exception("Günlük fiyat 0`dan büyük olmalıdır.");
            }
            this.Plaka = plaka;
            this.Marka=marka;
            this.Model = model;  
            this.GunlukFiyat = gunlukFiyat;  
            this.Tip = tip;
        }
    }

}




