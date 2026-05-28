# 🚗 Akıllı Araç Kiralama Sistemi

> C# ile geliştirilmiş, JSON tabanlı kalıcı depolama özelliğine sahip konsol uygulaması.

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-7.3-239120?style=flat-square&logo=csharp)
![Newtonsoft.Json](https://img.shields.io/badge/Newtonsoft.Json-13.0.4-orange?style=flat-square)
![Lisans](https://img.shields.io/badge/Lisans-MIT-blue?style=flat-square)

---

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Özellikler](#-özellikler)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Sınıf Yapısı](#-sınıf-yapısı)
- [Dosya Yapısı](#-dosya-yapısı)
- [Teknik Detaylar](#-teknik-detaylar)

---

## 📖 Proje Hakkında

Bu proje, C# programlama dili kullanılarak geliştirilmiş bir araç kiralama yönetim sistemidir. Nesne yönelimli programlama (OOP) prensipleri, hata yönetimi ve dosya tabanlı veri kalıcılığı konularını pratikte uygulamak amacıyla tasarlanmıştır.

Sistem, araçları ve rezervasyonları JSON formatında diske kaydederek uygulama kapatılıp açılsa dahi verilerin korunmasını sağlar.

---

<img width="1482" height="762" alt="image" src="https://github.com/user-attachments/assets/e9ef0a0b-8e52-439e-b381-89c140c3c54c" />

## ✨ Özellikler

| Özellik | Açıklama |
|---|---|
| **Araç Yönetimi** | Plaka, marka, model, fiyat ve araç tipi ile kayıt oluşturma |
| **Müsaitlik Kontrolü** | Verilen tarih aralığında uygun araçları listeleme |
| **Rezervasyon** | Tarih çakışması kontrolü ve otomatik ücret hesaplama |
| **İptal İşlemi** | Gelecek tarihli rezervasyonları plakaya göre iptal etme |
| **Raporlama** | Toplam ciro ve en çok kiralanan araç istatistikleri |
| **Kalıcı Depolama** | Tüm veriler JSON dosyalarında saklanır |
| **Hata Yönetimi** | Geçersiz giriş ve çakışan rezervasyon durumları için kapsamlı doğrulama |

---

## 🛠 Kurulum

### Gereksinimler

- [Visual Studio 2019 veya üzeri](https://visualstudio.microsoft.com/) (Community sürümü ücretsizdir)
- .NET Framework 4.7.2
- NuGet paket yöneticisi (Visual Studio ile birlikte gelir)

### Adım 1 — Projeyi Klonla

```bash
git clone https://github.com/kullanici-adi/arac-kiralama-sistemi.git
cd arac-kiralama-sistemi
```

### Adım 2 — Visual Studio ile Aç

```
BitirmeProjesi.sln dosyasını Visual Studio ile açın.
```

### Adım 3 — NuGet Paketini Yükle

Visual Studio içinde:

```
Araçlar → NuGet Paket Yöneticisi → Paket Yöneticisi Konsolu
```

Ardından konsola şunu yazın:

```powershell
Install-Package Newtonsoft.Json -Version 13.0.4
```

Ya da `packages.config` dosyası zaten projede mevcut olduğundan Visual Studio açılışta paketi otomatik olarak yükleyecektir.

### Adım 4 — Çalıştır

`F5` tuşuna basın veya **Hata Ayıklama → Hata Ayıklamayı Başlat** menüsünü kullanın.

---

## 🖥 Kullanım

Uygulama başlatıldığında aşağıdaki menü görüntülenir:

```
==========================================
===  AKILLI ARAÇ KİRALAMA SİSTEMİ  ===
==========================================
1. Yeni Araç Ekle
2. Müsait Araçları Listele
3. Rezervasyon Yap
4. Rezervasyon İptal Et
5. Raporlar
0. Çıkış
Seçiminiz:
```

### Örnek Kullanım Senaryosu

**1. Araç Ekleme**
```
Plaka: 34ABC01
Marka: Toyota
Model: Corolla
Günlük Fiyat: 1200
Araç Tipi (0: Sedan, 1: SUV, 2: Hatchback, 3: Ticari): 0
→ Araç başarıyla eklendi.
```

**2. Müsait Araçları Listeleme**
```
Başlangıç Tarihi: 01.07.2025
Bitiş Tarihi: 05.07.2025
→ 34ABC01 - Toyota Corolla (Sedan) - 1200 TL
→ 06XYZ99 - Audi Q7 (SUV) - 2500 TL
```

**3. Rezervasyon Yapma**
```
Müşteri Adı: Ahmet Yılmaz
Plaka: 34ABC01
Başlangıç: 01.07.2025
Bitiş: 05.07.2025
Tahmini Tutar: 4800 TL
Onay (E/H): E
→ Rezervasyon yapıldı.
```

---

## 🏗 Sınıf Yapısı

### `Arac`

Sistemdeki bir aracı temsil eden varlık sınıfıdır.

| Özellik | Tür | Açıklama |
|---|---|---|
| `Plaka` | `string` | Araç plakası — boş bırakılamaz |
| `Marka` | `string` | Araç markası |
| `Model` | `string` | Araç modeli |
| `GunlukFiyat` | `double` | Kiralık günlük ücret — 0'dan büyük olmalı |
| `Tip` | `AracTipi` | Enum: Sedan, SUV, Hatchback, Ticari |

**Doğrulama kuralları:**
- Plaka boş bırakılırsa `Exception` fırlatılır.
- Günlük fiyat sıfır veya negatif girilirse `Exception` fırlatılır.

---

### `Rezervasyon`

Bir kiralama işlemini temsil eden kayıt sınıfıdır.

| Özellik | Tür | Açıklama |
|---|---|---|
| `Id` | `Guid` | Her rezervasyon için otomatik üretilen benzersiz kimlik |
| `MusteriAdi` | `string` | Kiralayan müşterinin adı |
| `AracPlaka` | `string` | Kiralanan aracın plakası |
| `Baslangic` | `DateTime` | Kiralama başlangıç tarihi |
| `Bitis` | `DateTime` | Kiralama bitiş tarihi |
| `ToplamUcret` | `double` | Hesaplanan toplam kiralama ücreti |

**Ücret hesaplama mantığı:**
- Gün sayısı = bitiş − başlangıç (kısmi günler tam gün sayılır)
- Minimum kiralama süresi: 1 gün
- `ToplamUcret` = gün sayısı × günlük fiyat

---

### `KiralamaServisi`

Tüm iş mantığını barındıran servis katmanıdır.

| Metot | Dönüş Tipi | Açıklama |
|---|---|---|
| `AracEkle(plaka, marka, model, fiyat, tip)` | `void` | Yeni araç ekler; aynı plaka varsa hata fırlatır |
| `MusaitAraclariGetir(baslangic, bitis)` | `List<string>` | Belirtilen tarihler için müsait araçları listeler |
| `AracMusaitMi(plaka, bas, bit)` | `bool` | Bir aracın verilen tarih aralığında boş olup olmadığını kontrol eder |
| `RezervasyonEkle(musteri, plaka, bas, bit)` | `void` | Müsaitlik kontrolü yaparak rezervasyon oluşturur |
| `RezervasyonUcretiHesapla(plaka, bas, bit)` | `double` | Rezervasyon öncesi tahmini ücret döner |
| `RezervasyonIptal(plaka)` | `void` | Plakaya ait gelecek tarihli rezervasyonu iptal eder |
| `ToplamGelir()` | `double` | Tüm rezervasyonların toplam ücretini döner |
| `EnCokKiralananArac()` | `string` | En yüksek rezervasyon sayısına sahip aracı bulur |
| `MusteriRezervasyonlariniGetir(musteri)` | `List<string>` | Belirli bir müşterinin tüm rezervasyonlarını listeler |
| `VerileriKaydet()` | `void` | Araç ve rezervasyon listelerini JSON'a yazar |
| `VerileriYukle()` | `void` | Uygulama başlangıcında JSON dosyalarından veri okur |

---

## 📁 Dosya Yapısı

```
BitirmeProjesi/
│
├── BitirmeProjesi.sln          # Visual Studio çözüm dosyası
├── BitirmeProjesi.csproj       # Proje dosyası
│
├── Arac.cs                     # Arac sınıfı ve AracTipi enum
├── Rezervasyon.cs              # Rezervasyon sınıfı
├── KiralamaServisi.cs          # İş mantığı ve JSON işlemleri
├── Program.cs                  # Giriş noktası ve konsol menüsü
│
├── App.config                  # .NET Framework yapılandırması
├── packages.config             # NuGet bağımlılıkları
│
├── araclar.json                # (Çalışma zamanında oluşur) Araç verileri
└── rezervasyonlar.json         # (Çalışma zamanında oluşur) Rezervasyon verileri
```

---

## ⚙️ Teknik Detaylar

### Veri Kalıcılığı

Araç ve rezervasyon verileri `Newtonsoft.Json` kütüphanesi kullanılarak JSON formatında diske yazılır. Uygulama her başlatıldığında bu dosyalar otomatik olarak okunur.

```csharp
// Kaydetme
string json = JsonConvert.SerializeObject(Araclar);
File.WriteAllText("araclar.json", json);

// Yükleme
string json = File.ReadAllText("araclar.json");
Araclar = JsonConvert.DeserializeObject<List<Arac>>(json);
```

### Tarih Çakışması Algoritması

İki tarih aralığının çakışıp çakışmadığı aşağıdaki mantıkla tespit edilir:

```csharp
// Çakışma YOKSA: yeni bitiş <= mevcut başlangıç VEYA yeni başlangıç >= mevcut bitiş
// Çakışma VARSA: bu koşulun tersi
if (!(bitisYeni <= r.Baslangic || baslangicYeni >= r.Bitis))
    return false; // Araç müsait değil
```

### Hata Yönetimi

Tüm kullanıcı girişleri `try-catch` bloklarıyla sarmalanmıştır. Oluşan hatalar kullanıcıya açıklayıcı mesajlarla gösterilir, uygulama çökmez.

---

## 📄 Lisans

Bu proje MIT lisansı altında dağıtılmaktadır. Daha fazla bilgi için `LICENSE` dosyasına bakınız.

---

<div align="center">
  <sub>C# Bitirme Projesi — Nesne Yönelimli Programlama</sub>
</div>
