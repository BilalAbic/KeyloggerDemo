Windows Keyboard Event Logger — Eğitim Amaçlı (README)

Uyarı (Kesin): Bu proje yalnızca eğitim, araştırma ve savunma amaçlıdır. Kişisel verilerin izinsiz kaydedilmesi, paylaşılması veya kötüye kullanımı suçtur. Projeyi yalnızca kendi kontrolünüzdeki, izole edilmiş ortamlar (ör. VM) ve açık rızası olan test hesapları üzerinde çalıştırın. Bu proje, izinsiz gözetim veya veri toplama amaçlarına yönelik kullanılmamalıdır.

1. Proje Özeti

Ad: Windows Keyboard Event Logger (Eğitim amaçlı)

Teknoloji: C# (.NET Framework) — Windows Forms

Amaç: Windows düşük seviyeli klavye kancası (low-level keyboard hook) mekanizmasının, ToUnicode dönüşümlerinin ve durum tuşu (Shift / CapsLock / Alt) yönetiminin eğitimsel olarak incelenmesi.

Durum: Sadece klavye yakalama ve GUI gösterimi uygulanmış; ileri veri aktarımı, kalıcılık veya dışa aktarma özellikleri henüz eklenmemiştir.

2. Kapsam ve Kullanım Politikası

Projenin tek geçerli kullanımı: eğitim, test, araştırma ve savunma amaçlı kod incelemeleri.

Yasaklar: Başkalarının rızası olmadan çalıştırma, ağ üzerinden veri sızdırma, kötü amaçlı entegrasyon veya üretim ortamına dağıtım.

Kullanım sırasında ilgili ülkenin yasalarına (ör. Türkiye'de Türk Ceza Kanunu, KVKK) ve kurumunuzun bilgi güvenliği politikalarına uyun.

3. Mevcut Özellikler (Şu anki kod)

Sistem düzeyinde klavye kancası kurma (SetWindowsHookEx).

Basılan tuşları ToUnicode ile karaktere çevirme.

Shift, CapsLock ve Alt durumlarını kontrol etme.

Basılan karakterleri RichTextBox üzerinde gösterme.

Noktalama işaretlerinden sonra satır sonu ekleme mantığı.

Proje bağlantıları için Process.Start kullanan örnek LinkLabel handler'ları.

4. Henüz Eklenmeyen / Bilinçli Olarak Hariç Tutulan Özellikler

Aşağıdaki özellikler bu eğitim projesinde bilerek yer almamalıdır veya yalnızca denetimli ortamlarda ve net onayla eklenmelidir:

Uzaktan veri aktarımı (ağ üzerinden gönderim, telemetry, FTP/HTTP exfiltration).

Arka plan çalışan, başlatma zamanına kalıcı ekleme (persistence).

Gizleme/obfuscation, anti-forensics, keylogger’ı tespitten kaçırma teknikleri.

Kullanıcıların izni olmadan veri saklama veya üçüncü taraflarla paylaşım.

5. Güvenli Test ve Geliştirme Rehberi (Zorunlu)

Projenin güvenli, etik ve yasal bir şekilde incelenmesi için izlenecek asgari prosedür:

İzole Ortam: Her zaman sanal makine (VM) veya izole test makinesi kullanın. Host üzerinde çalıştırmayın.

Kullanıcı/Rıza: Test kullanıcıları açıkça bilgilendirilmeli ve yazılı onay alınmalıdır.

Veri: Gerçek kişisel veriler (şifre, banka bilgisi, kimlik numaraları) kullanılmaz. Test verileri oluşturun.

Ağ Erişimi: Test VM’nin ağ erişimini kısıtlayın; gerekiyorsa tamamen kapatın.

Log Tutma & Silme: Deney sonrası kaydedilen herhangi bir veri derhal silinir; silme prosedürü belgeye bağlanır.

İzleme & Denetim: Deney sırasında erişim ve etkinlikler kaydedilir; yetkisiz kullanım hızlıca raporlanır.

6. Güvenlik Tavsiyeleri (Geliştirici Perspektifi)

Kaydetme/aktarma özellikleri eklemeden önce gizlilik, şifreleme ve erişim kontrolü politikalarını belirleyin.

Proje asla default olarak hassas veri saklamamalı; kullanıcı ayarları ve izinler açıkça yönetilmeli.

Kodun kötüye kullanım riskleri için kod incelemesi ve dış güvenlik denetimi planlayın.

Her zaman audit trail ve erişim kayıtları tutun; denetim belgelerini saklayın.

7. Derleme ve Çalıştırma (Eğitimsel)

UYARI: Aşağıdaki adımları yalnızca izole ve izinli test ortamında uygulayın.

Visual Studio ile projeyi açın (sln dosyası).

Hedef framework .NET Framework 4.7.2+ olarak ayarlı olmalıdır.

Debug modunda başlatın. Yönetici yetkisi gerekliyse Visual Studio'yu yönetici olarak çalıştırın.

Test senaryoları ile tuş yakalama davranışını gözlemleyin.

8. Katkı (Contributing)

Katkı kabul edilir ancak gizlilik ve etik kurallar çerçevesinde sınırlıdır.

Pull request gönderirken amaç, test ortamı ve alınan onaylar açıkça belirtilmelidir.

Kötüye kullanım potansiyeli taşıyan değişiklikler rededilebilir.

9. Lisans

Proje MIT lisansı ile lisanslanmıştır (veya proje sahibinin tercih ettiği açık lisans). Lisans, kodun eğitim amaçlı kopyalanmasına izin verir; kötüye kullanım sorumluluğu kullanıcıya aittir.

10. İletişim

Geliştirici: Bilal

GitHub: https://github.com/BilalAbic

E-posta (iş): bilalabic78[@]gmail.com