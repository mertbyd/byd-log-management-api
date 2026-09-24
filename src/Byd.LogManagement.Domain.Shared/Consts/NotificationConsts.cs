using System.Collections.Generic;

namespace Byd.LogManagement.Consts;

/// <summary>
/// Bildirimin tablo, kolon ve lookup kod sabitlerini tanimlar.
/// </summary>
// islevi: Bildirim tablolarinin kalicilik sabitlerini ve lookup satirlarinin is kodlarini tek yerde tutar.
// sistemdeki gorevi: EF konfigurasyonu, uretici uygulama ve seed akisinin ayni degerleri okumasini saglar;
// lookup satirinin kimligi ortamdan ortama degisir, is kodu degismez.
public static class NotificationConsts
{
    // Alici basina yazilan bildirim satirlarinin tutuldugu tablonun EF model adidir.
    public const string TableName = "Notifications";

    // Bildirim kanali lookup tablosunun EF model adidir.
    public const string ChannelTableName = "NotificationChannels";

    // Mobil bildirimlerin gonderildigi Firebase projesi tablosunun EF model adidir.
    public const string FirebaseProjectTableName = "FirebaseProjects";

    // Firebase projesi benzersiz anahtari (slug) icin ust sinir.
    public const int MaxFirebaseProjectKeyLength = 64;

    // Firebase proje anahtari deseni (kucuk harf, rakam, alt cizgi, tire).
    public const string FirebaseProjectKeyPattern = "^[a-z0-9-_]+$";

    // Firebase proje calisma ortami adi icin ust sinir.
    public const int MaxEnvironmentLength = 32;

    // Bildirim gonderiminin ayrildigi iki calisma ortami. Ayni anahtar iki ortamda ayri projeyi gosterebilsin diye
    // her satir bu iki degerden biriyle damgalanir; deger cagirandan alinmaz, sunucunun ortamindan cozulur.
    public const string TestEnvironment = "test";

    public const string ProductionEnvironment = "prod";

    // Host'un beyan ettigi ortam adlarindan uretim sayilani; bunun disindaki her ad test kovasina duser.
    public const string ProductionHostEnvironmentName = "Production";

    // FCM servis hesabi dosyalarinin dosya modulundeki kategori adi; dosya modulu kategoriyi adla adresler.
    public const string FirebaseSecretFileCategoryName = "Firebase project secrets";

    // Mobil gonderim denemelerinin sonucunun tutuldugu tablonun EF model adidir.
    public const string MobileNotificationTableName = "MobileNotifications";

    // Mobil gonderimin hedefinin (cihaz token'i ya da topic) ust siniri.
    public const int MaxTargetLength = 512;

    // Projenin FCM secret'i yuklenmediginde gonderim gunlugune yazilan durum mesajidir.
    public const string MissingSecretStatusMessage = "FirebaseSecretNotFound";

    // Secret gecerli bir servis hesabi olmadigi icin gonderim yapilamadiginda gunluge yazilan durum kodudur.
    public const string InvalidCredentialStatusCode = "InvalidCredential";

    // FCM'in gonderim sonucunda dondurdugu hata kodunun ust siniri.
    public const int MaxStatusCodeLength = 128;

    // FCM'in gonderim sonucunda dondurdugu hata mesajinin ust siniri.
    public const int MaxErrorMessageLength = 1024;

    // Topic adinin ust siniri; ad ayni zamanda gonderim gunlugunun hedef kolonuna yazilir.
    public const int MaxTopicNameLength = 128;

    // FCM'in topic adinda kabul ettigi karakterlerin deseni.
    public const string TopicNamePattern = "^[a-zA-Z0-9-_.~%]+$";

    // Kullanici mobil push bildirim kayit token'larinin tutuldugu tablonun EF model adidir.
    public const string UserMobileTokenTableName = "UserMobileTokens";

    // FCM mobil kayit token'inin ust siniri.
    public const int MaxDeviceTokenLength = 512;

    // Cihaz dil kodunun ust siniri ("tr", "en-US" gibi).
    public const int MaxLanguageCodeLength = 10;

    // FCM'in tek multicast isteginde kabul ettigi en fazla kayit token'i sayisi.
    public const int MaxFirebaseTokensPerRequest = 500;

    // Uretici modulun icerik sozlugunun saklandigi kolon tipi.
    public const string DataColumnType = "jsonb";

    // Lookup kayitlarinin gorunen adi icin ust sinir.
    public const int MaxLookupNameLength = 50;

    // Tarayici uzerinden gonderilen bildirim kanalinin is kodu.
    public const int WebChannelCode = 1;

    // Mobil uygulamaya gonderilen bildirim kanalinin is kodu.
    public const int MobileChannelCode = 2;

    // Seed edilen kanal satirlarinin is kodu -> gorunen ad eslemesi.
    public static readonly IReadOnlyDictionary<int, string> ChannelNames = new Dictionary<int, string>
    {
        [WebChannelCode] = "Web",
        [MobileChannelCode] = "Mobile"
    };

    // Web istemcisine iletilen bildirim olayinin sabit adidir.
    public const string WebMessageName = "notificationReceived";

    // Tenant bildirimlerinin gittigi SignalR grubunun on ekidir; grup adi "<onek>:<tenantId>" seklindedir.
    public const string TenantGroupPrefix = "notification-tenant";

    // Ileri tarihli web bildirimlerinin vadesi gelmis mi diye tarandigi aralik (saniye).
    public const int ScheduledScanPeriodSeconds = 10;
}
