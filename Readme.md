Windows Keyboard Event Logger — Eğitim Amaçlı

**Uyarı:** Bu proje **sadece eğitim ve test amaçlıdır**. Başka bir kişinin bilgisi ve izni olmadan kullanımı yasa dışıdır ve etik dışıdır. Bu kodu izinsiz kullanmaktan doğacak tüm hukuki ve etik sorumluluklar kullanıcıya aittir.

## Özet

Windows işletim sistemi üzerinde çalışan bu uygulama, global keyboard hook ile tuş girişlerini yakalar, cümle bazında (nokta, soru işareti, ünlem veya yeni satır ile) tamponlayarak SQL Server veritabanına kaydeder ve belirlenen aralıklarla batch halinde Gmail SMTP üzerinden e-posta ile gönderir. Ayrıca uygulama içinde kullanıcı veri silme talebi gönderebilecek bir arayüz bulunmaktadır.

## Özellikler

- Global low-level keyboard hook ile tuş yakalama (WH_KEYBOARD_LL).
- Karakterleri `ToUnicode` ile yerel klavye düzenine göre çözümleme (shift, caps lock durumu dikkate alınır).
- Cümle bazında tamponlama ve veritabanına kayıt (TBL_LOGGER).
- Batch tabanlı e-posta gönderimi (Gmail SMTP, app.config ile konfigüre edilebilir).
- Gönderim deneme sayısı yönetimi; başarısız denemelerde `sendAttempts` arttırılır.
- Form kapanışında buffer temizliği ve hook kaldırma işlemleri ile güvenli kapanış.
- Kullanıcıdan gelen veri silme talebini e-posta ile yöneticilere iletme.

## Gereksinimler

- .NET Framework 4.8
- Visual Studio (veya uyumlu IDE)
- SQL Server (LocalDB / Local / Uzak)
- Gmail hesabı ve App Password (SMTP için)

## Veritabanı - Yapı

Aşağıdaki SQL, gerekli tabloyu oluşturmak içindir. `DboKeylogger` isimli veritabanını yaratıp tabloyu ekleyin.

```sql
CREATE DATABASE DboKeylogger;
GO

USE DboKeylogger;
GO

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