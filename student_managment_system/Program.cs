namespace student_managment_system
{
    internal class Program
    {
        private static string klasor = "ogrenciler";
        static void Main(string[] args)
        {
            Directory.CreateDirectory(klasor);
            while (true)
            {
                ShowMenu();
                if (!int.TryParse(Console.ReadLine(), out int secim))
                {
                    Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                    continue;
                }

                Console.Clear();
                switch (secim)
                {
                    case 0: return;
                    case 1: OgrenciIslemleri.Ekle(klasor); break;
                    case 2: OgrenciIslemleri.Sil(klasor); break;
                    case 3: OgrenciIslemleri.Listele(klasor); break;
                    case 4: OgrenciIslemleri.Goster(klasor); break;
                    case 5: OgrenciIslemleri.NotGir(klasor); break;
                    case 6: OgrenciIslemleri.DevamsizlikEkle(klasor); break;
                    case 7: OgrenciIslemleri.BasariRaporu(klasor); break;
                    case 8: OgrenciIslemleri.EnBasarili(klasor); break;
                    case 9: OgrenciIslemleri.EnBasarisiz(klasor); break;
                    case 10: OgrenciIslemleri.SinifOrtalamasi(klasor); break;
                    case 11: OgrenciIslemleri.NotHistogram(klasor); break;
                    case 12: OgrenciIslemleri.NotGrafigi(klasor); break;
                    case 13:
                        Console.WriteLine($"Toplam öğrenci sayısı: {OgrenciIslemleri.ToplamSayisi(klasor)}");
                        break;
                    case 99:
                        OgrenciIslemleri.OrnekOgrenciOlustur(klasor);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
                        break;
                }

                Console.WriteLine("Devam etmek için bir tuşa basın...");
                Console.ReadKey();
                Console.Clear();
            }

        }
        static void ShowMenu()
        {
            Console.WriteLine("\n📘 ÖĞRENCİ YÖNETİM SİSTEMİ");
            Console.WriteLine("1. Öğrenci Ekle");
            Console.WriteLine("2. Öğrenci Sil");
            Console.WriteLine("3. Tüm Öğrencileri Listele");
            Console.WriteLine("4. Öğrenci Bilgilerini Göster");
            Console.WriteLine("5. Not Girişi");
            Console.WriteLine("6. Devamsızlık Ekle");
            Console.WriteLine("7. Başarı Raporu Oluştur");
            Console.WriteLine("8. En Başarılı Öğrenciyi Bul");
            Console.WriteLine("9. En Başarısız Öğrenciyi Bul");
            Console.WriteLine("10. Sınıf Ortalamasını Göster");
            Console.WriteLine("11. Not Ortalaması Histogramı");
            Console.WriteLine("12. Öğrenci Not Grafiği");
            Console.WriteLine("13. Toplam Öğrenci Sayısı");
            Console.WriteLine("99. Örnek Öğrenci Oluştur (Test için)");
            Console.WriteLine("0. Çıkış");
            Console.Write("Seçiminiz: ");
        }
        static class OgrenciIslemleri
        {
            public static void Ekle(string klasor)
            {
                Console.Write("Öğrenci numarası: ");
                string numara = Console.ReadLine();
                Console.Write("Ad: ");
                string ad = Console.ReadLine();
                Console.Write("Soyad: ");
                string soyad = Console.ReadLine();
                string yol = Path.Combine(klasor, numara + ".txt");

                if (File.Exists(yol))
                {
                    Console.WriteLine("❗ Bu öğrenci zaten kayıtlı.");
                    return;
                }

                string[] bilgiler =
                {
                $"Ad: {ad}",
                $"Soyad: {soyad}",
                $"Numara: {numara}",
                "Vize: 0",
                "Final: 0",
                "Devamsizlik: 0"
            };
                File.WriteAllLines(yol, bilgiler);
                Console.WriteLine("✅ Öğrenci eklendi.");
            }

            public static void Sil(string klasor)
            {
                Console.Write("Silinecek öğrenci numarası: ");
                string numara = Console.ReadLine();
                string yol = Path.Combine(klasor, numara + ".txt");

                if (!File.Exists(yol))
                {
                    Console.WriteLine("❗ Öğrenci bulunamadı.");
                    return;
                }

                File.Delete(yol);
                Console.WriteLine("🗑️ Öğrenci silindi.");
            }

            public static void Listele(string klasor)
            {
                var dosyalar = Directory.GetFiles(klasor);
                if (dosyalar.Length == 0)
                {
                    Console.WriteLine("📂 Hiç öğrenci yok.");
                    return;
                }

                foreach (var dosya in dosyalar)
                {
                    var bilgiler = File.ReadAllLines(dosya);
                    if (bilgiler.Length >= 3)
                        Console.WriteLine($"{bilgiler[2]}, {bilgiler[0]}, {bilgiler[1]}");
                }
            }

            public static void Goster(string klasor)
            {
                Console.Write("Bilgileri gösterilecek öğrenci numarası: ");
                string numara = Console.ReadLine();
                string yol = Path.Combine(klasor, numara + ".txt");

                if (!File.Exists(yol))
                {
                    Console.WriteLine("❗ Öğrenci bulunamadı.");
                    return;
                }

                Console.WriteLine("\n🎓 Öğrenci Bilgileri:");
                foreach (var satir in File.ReadAllLines(yol))
                    Console.WriteLine(satir);
            }

            public static void NotGir(string klasor)
            {
                Console.Write("Not girilecek öğrenci numarası: ");
                string numara = Console.ReadLine();
                string yol = Path.Combine(klasor, numara + ".txt");

                if (!File.Exists(yol))
                {
                    Console.WriteLine("❗ Öğrenci bulunamadı.");
                    return;
                }

                Console.Write("Vize notu: ");
                int vize = int.Parse(Console.ReadLine());
                Console.Write("Final notu: ");
                int final = int.Parse(Console.ReadLine());

                var bilgiler = File.ReadAllLines(yol);
                bilgiler[3] = $"Vize: {vize}";
                bilgiler[4] = $"Final: {final}";
                File.WriteAllLines(yol, bilgiler);
                Console.WriteLine("✅ Notlar kaydedildi.");
            }

            public static void DevamsizlikEkle(string klasor)
            {
                Console.Write("Devamsızlık eklenecek öğrenci numarası: ");
                string numara = Console.ReadLine();
                string yol = Path.Combine(klasor, numara + ".txt");

                if (!File.Exists(yol))
                {
                    Console.WriteLine("❗ Öğrenci bulunamadı.");
                    return;
                }

                Console.Write("Eklenecek devamsızlık sayısı: ");
                int eklenecek = int.Parse(Console.ReadLine());
                var bilgiler = File.ReadAllLines(yol);
                int mevcut = int.Parse(bilgiler[5].Split(':')[1].Trim());
                bilgiler[5] = $"Devamsizlik: {mevcut + eklenecek}";
                File.WriteAllLines(yol, bilgiler);
                Console.WriteLine("✅ Devamsızlık güncellendi.");
            }

            public static void BasariRaporu(string klasor)
            {
                var dosyalar = Directory.GetFiles(klasor);
                foreach (var dosya in dosyalar)
                {
                    var b = File.ReadAllLines(dosya);
                    string ad = b[0].Split(':')[1].Trim();
                    string soyad = b[1].Split(':')[1].Trim();
                    string numara = b[2].Split(':')[1].Trim();
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    int devamsizlik = int.Parse(b[5].Split(':')[1]);
                    double ort = vize * 0.4 + final * 0.6;
                    string durum = (devamsizlik >= 4 || ort < 60) ? "Kaldı" : "Geçti";
                    Console.WriteLine($"{numara} {ad} {soyad} | Ortalama: {ort} | Durum: {durum}");
                }
            }

            public static void EnBasarili(string klasor)
            {
                double max = -1;
                string ogr = "";
                foreach (var dosya in Directory.GetFiles(klasor))
                {
                    var b = File.ReadAllLines(dosya);
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    double ort = vize * 0.4 + final * 0.6;
                    if (ort > max)
                    {
                        max = ort;
                        ogr = b[2];
                    }
                }
                Console.WriteLine($"🏆 En başarılı öğrenci: {ogr} | Ortalama: {max}");
            }

            public static void EnBasarisiz(string klasor)
            {
                double min = 101;
                string ogr = "";
                foreach (var dosya in Directory.GetFiles(klasor))
                {
                    var b = File.ReadAllLines(dosya);
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    double ort = vize * 0.4 + final * 0.6;
                    if (ort < min)
                    {
                        min = ort;
                        ogr = b[2];
                    }
                }
                Console.WriteLine($"📉 En başarısız öğrenci: {ogr} | Ortalama: {min}");
            }

            public static void SinifOrtalamasi(string klasor)
            {
                var dosyalar = Directory.GetFiles(klasor);
                double toplam = 0;
                int sayi = 0;
                foreach (var dosya in dosyalar)
                {
                    var b = File.ReadAllLines(dosya);
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    toplam += vize * 0.4 + final * 0.6;
                    sayi++;
                }
                Console.WriteLine($"📊 Sınıf Ortalaması: {toplam / sayi:0.00}");
            }

            public static void NotHistogram(string klasor)
            {
                int[] histogram = new int[10];
                foreach (var dosya in Directory.GetFiles(klasor))
                {
                    var b = File.ReadAllLines(dosya);
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    int index = (int)((vize * 0.4 + final * 0.6) / 10);
                    if (index >= 10) index = 9;
                    histogram[index]++;
                }
                Console.WriteLine("📈 Not Histogramı:");
                for (int i = 0; i < histogram.Length; i++)
                {
                    Console.WriteLine($"{i * 10}-{i * 10 + 9}: {new string('*', histogram[i])}");
                }
            }

            public static void NotGrafigi(string klasor)
            {
                foreach (var dosya in Directory.GetFiles(klasor))
                {
                    var b = File.ReadAllLines(dosya);
                    string num = b[2].Split(':')[1].Trim();
                    int vize = int.Parse(b[3].Split(':')[1]);
                    int final = int.Parse(b[4].Split(':')[1]);
                    double ort = vize * 0.4 + final * 0.6;
                    Console.WriteLine($"{num}: {new string('#', (int)(ort / 2))} ({ort})");
                }
            }

            public static int ToplamSayisi(string klasor)
            {
                return Directory.GetFiles(klasor).Length;
            }

            public static void OrnekOgrenciOlustur(string klasor)
            {
                string yol = Path.Combine(klasor, "123456.txt");
                if (!File.Exists(yol))
                {
                    string[] bilgiler =
                    {
                    "Ad: Test",
                    "Soyad: Ogrenci",
                    "Numara: 123456",
                    "Vize: 70",
                    "Final: 80",
                    "Devamsizlik: 1"
                };
                    File.WriteAllLines(yol, bilgiler);
                    Console.WriteLine("🧪 Test öğrencisi oluşturuldu.");
                }
            }
        }
    }
}
