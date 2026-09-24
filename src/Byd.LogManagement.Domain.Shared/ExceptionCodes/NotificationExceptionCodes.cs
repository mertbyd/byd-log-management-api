namespace Byd.LogManagement.ExceptionCodes;

/// <summary>
/// Bildirimin is ve girdi dogrulama hata kodlarini tanimlar.
/// </summary>
// islevi: Firebase proje adi ve kodu icin zorunluluk ve uzunluk hatalarinin kararli kodlarini tutar.
// sistemdeki gorevi: Validator'larin ve Manager'in kullandigi kodlarin tek kaynagidir; her kodun
// localization JSON dosyalarinda ayni anahtarla bir karsiligi bulunur.
public static class NotificationExceptionCodes
{
    // Firebase proje adi bos gecilemez.
    public const string FirebaseProjectNameRequired = LogManagementErrorCodes.NotificationGroup + "0001";

    // Firebase proje adi lookup ad sinirini asamaz.
    public const string FirebaseProjectNameTooLong = LogManagementErrorCodes.NotificationGroup + "0002";

    // Uretici bildirimde projeyi bu anahtarla adresler; anahtarsiz projeye bildirim gonderilemez.
    public const string FirebaseProjectKeyRequired = LogManagementErrorCodes.NotificationGroup + "0003";

    // Firebase proje anahtari ust siniri asamaz.
    public const string FirebaseProjectKeyTooLong = LogManagementErrorCodes.NotificationGroup + "0015";

    // Firebase proje anahtari yalnizca kucuk harf, rakam, alt cizgi ve tire icerebilir.
    public const string FirebaseProjectKeyInvalid = LogManagementErrorCodes.NotificationGroup + "0016";



    // Yuklenen FCM secret dosyasinin adi bos gecilemez.
    public const string FirebaseSecretFileNameRequired = LogManagementErrorCodes.NotificationGroup + "0004";

    // Yuklenen FCM secret dosyasinin icerigi bos gecilemez.
    public const string FirebaseSecretDataRequired = LogManagementErrorCodes.NotificationGroup + "0005";

    // Mobil token bos gecilemez.
    public const string DeviceTokenRequired = LogManagementErrorCodes.NotificationGroup + "0008";

    // Mobil token ust siniri asamaz.
    public const string DeviceTokenTooLong = LogManagementErrorCodes.NotificationGroup + "0009";

    // Cihaz dil kodu ust siniri asamaz.
    public const string LanguageCodeTooLong = LogManagementErrorCodes.NotificationGroup + "0010";

    // Topic adi bos gecilemez.
    public const string TopicNameRequired = LogManagementErrorCodes.NotificationGroup + "0012";

    // Topic adi ust siniri asamaz.
    public const string TopicNameTooLong = LogManagementErrorCodes.NotificationGroup + "0013";

    // Topic adi FCM'in kabul ettigi karakterlerden olusmalidir.
    public const string TopicNameInvalid = LogManagementErrorCodes.NotificationGroup + "0014";
}
