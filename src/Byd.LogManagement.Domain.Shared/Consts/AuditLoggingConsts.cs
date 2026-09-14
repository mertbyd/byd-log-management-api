namespace Byd.LogManagement.Consts;

/// <summary>
/// Audit log kaydina modulun ekledigi ozelliklerin kararli adlarini tanimlar.
/// </summary>
// islevi: Audit kaydinin ExtraProperties sozlugune yazilan anahtar adlarini tek yerde tutar.
// sistemdeki gorevi: Anahtarlari yazan contributor ile ileride onlari okuyacak sorgu ve DTO'larin ortak sozlesmesidir.
public static class AuditLoggingConsts
{
    // Kaydin organizasyon kapsamini tasiyan ozelligin kararli adi.
    public const string OrganizationIdPropertyName = "OrganizationId";

    // Kaydin organizasyon adini tasiyan ozelligin kararli adi.
    public const string OrganizationNamePropertyName = "OrganizationName";
}
