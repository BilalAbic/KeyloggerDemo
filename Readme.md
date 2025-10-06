# Keylogger Projesi

Bu proje, yalnızca **eğitimsel amaçlarla** geliştirilmiş bir Windows Forms tabanlı **C# Keylogger** uygulamasıdır. Uygulama, klavye girdilerini algılayarak bir metin kutusunda görüntüler ve aynı zamanda veritabanı veya e-posta ile loglama altyapısının nasıl çalıştığını öğretmeyi amaçlar. Proje, Nesne Tabanlı Programlama dersi kapsamında bir ödev olarak hazırlanmıştır.

---

## ⚙️ Proje Özellikleri

- **Klavye Girdilerini Dinleme:** Windows API (`SetWindowsHookEx`, `CallNextHookEx`, `UnhookWindowsHookEx`) kullanılarak düşük seviye klavye olaylarını yakalar.
- **Arayüz Tabanlı İzleme:** Girişler `RichTextBox` bileşenine gerçek zamanlı olarak yazılır.
- **Veritabanı veya Mail Desteği (Opsiyonel):** SqlConnection veya SMTP üzerinden logların iletilmesi yapılandırılabilir.
- **Invoke Kullanımı:** Form bileşenleri arasında thread-safe erişim için `Invoke` yöntemi uygulanır.
- **Regex Filtreleme:** Gereksiz karakterlerin ayıklanması ve okunabilirliğin artırılması sağlanır.
- **Arka Plan Çalışması:** Uygulama `Task` veya `Thread` ile arka planda çalıştırılabilir.
- **MAC Adresi Takibi:** Her log kaydı, sistemin donanım kimliği olarak kullanılan **cihazın MAC adresi** ile birlikte veritabanına eklenir; ayrıca **veri silme talebi e-postalarında** ilgili cihazın MAC adresi de otomatik olarak iletilir.

---

## 🧠 Eğitimsel Amaç

Bu proje, siber güvenlik ve sistem programlama konularında farkındalık kazandırmak amacıyla hazırlanmıştır. **Kullanıcı izni olmadan keylogger yazılımı geliştirmek veya dağıtmak yasal değildir.**  
Bu proje yalnızca etik ve laboratuvar ortamında incelenmelidir.

---

## 📦 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|------------|----------|
| C# .NET Framework | Uygulamanın ana geliştirme dili |
| Windows API | Klavye olaylarını yakalamak için |
| Windows Forms | Kullanıcı arayüzü |
| SQL / SMTP | Veri gönderimi (opsiyonel) |
| Ağ Arayüzü (MAC) | Donanım tabanlı cihaz kimliği izleme |

---

## 🧩 Kod Yapısı

```
Keylogger/
│
├── Form1.cs              # Ana form ve event hook yapısı
├── Program.cs            # Uygulama başlangıç noktası
├── App.config            # Veritabanı ve SMTP ayarları
└── README.md             # Proje dökümantasyonu
```

---

## 🚀 Çalıştırma Adımları

1. **Projeyi açın:** Visual Studio veya Rider gibi bir IDE kullanın.  
2. **Gerekli izinleri sağlayın:** Yönetici yetkileri gerekebilir.  
3. **App.config** dosyasındaki ayarları düzenleyin (isteğe bağlı olarak SQL veya Mail).
4. **Projeyi çalıştırın.**
5. **Klavye girişlerini izleyin.**

---

## 🧱 Alan Açıklamaları

- **`WH_KEYBOARD_LL`** → Klavye hook türünü tanımlar (düşük seviye).  
- **`CallNextHookEx`** → Sonraki hook’a olayın iletilmesini sağlar.  
- **`Marshal.GetDelegateForFunctionPointer`** → Fonksiyon işaretçilerinin yönetilmesi için kullanılır.  
- **`Invoke` / `MethodInvoker`** → Thread-safe arayüz güncellemesi sağlar.  
- **`Regex.Replace()`** → Metin temizleme ve biçimlendirme işlemleri yapılır.  
- **`SqlConnection` / `SqlCommand`** → Log kayıtlarını veritabanına gönderir.  
- **`SmtpClient` / `MailMessage`** → Logları e-posta ile gönderir.  
- **`NetworkInterface.GetAllNetworkInterfaces()`** → Cihazın MAC adresini alır ve log verilerine dahil eder.


---

## ⚠️ Yasal Uyarı

Bu yazılım **tamamen eğitimsel nitelikte** olup, kullanıcı verilerini izinsiz toplamak, paylaşmak veya kötüye kullanmak **suçtur**.  
Projeyi yalnızca **laboratuvar**, **akademik çalışma** veya **etik hackleme** kapsamında kullanınız.

---

## 📚 Lisans

Bu proje [MIT Lisansı](https://opensource.org/licenses/MIT) altında yayımlanmıştır.  
Her türlü ticari veya kötüye kullanım, geliştirici sorumluluğu dışındadır.

---

**Geliştirici:** Bilal  
**Amaç:** Eğitimsel ve akademik farkındalık  
**Dil:** C# (.NET Framework 4.8)  

---

## 📌 Bu proje, ilham kaynağı olarak [bu videodan](https://www.youtube.com/watch?v=j0sxcsxXJkY) yararlanılarak hazırlanmıştır.
