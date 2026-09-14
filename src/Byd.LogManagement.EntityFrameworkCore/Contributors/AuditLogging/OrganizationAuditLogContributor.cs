using Byd.LogManagement.Consts;
using Volo.Abp.Auditing;
using Volo.Abp.Data;

namespace Byd.LogManagement.EntityFrameworkCore;

/// <summary>
/// Audit kaydina organizasyon kapsamini ekler.
/// </summary>
// islevi: Kayit baslarken OrganizationId ve OrganizationName anahtarlarini audit bilgisinin ExtraProperties sozlugune yazar.
// sistemdeki gorevi: AuditLogs tablosundaki organizasyon kapsaminin tek yazaridir. ABP'nin donusturucusu sozlugu
// oldugu gibi AuditLog kaydina kopyalar; degerler tablonun ExtraProperties kolonunda yasar, ayri kolon yoktur.
// Ad id'nin yaninda yazma aninda saklanir; okuma aninda adi cozecek bir organizasyon kaynagi yoktur.
public class OrganizationAuditLogContributor : AuditLogContributor
{
    // Kapsam istegin basinda bellidir; ABP'nin HTTP katkicisi da istek bilgisini bu asamada yazar.
    public override void PreContribute(AuditLogContributionContext context)
    {
        // TODO: Organizasyon kaynagi - auth modulu organizasyon bilgisini sagladiginda (file ve core'daki
        // OrganizationId gibi) asagidaki iki null yazimi kaldirilir ve kaynak su iki yoldan biriyle baglanir.
        //
        // ICurrentOrganization ile:
        // var currentOrganization = context.ServiceProvider.GetRequiredService<ICurrentOrganization>();
        // context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationIdPropertyName, currentOrganization.Id);
        // context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationNamePropertyName, currentOrganization.Name);
        //
        // ICurrentUser claim'leri ile; claim tipi sabitleri auth modulunden gelir:
        // var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        // context.AuditInfo.SetProperty(
        //     AuditLoggingConsts.OrganizationIdPropertyName,
        //     ICurrentUser.FindClaimValue(OrganizationClaimTypes.OrganizationId));
        // context.AuditInfo.SetProperty(
        //     AuditLoggingConsts.OrganizationNamePropertyName,
        //     ICurrentUser.FindClaimValue(OrganizationClaimTypes.OrganizationName));
        context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationIdPropertyName, null);
        context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationNamePropertyName, null);
    }
}
