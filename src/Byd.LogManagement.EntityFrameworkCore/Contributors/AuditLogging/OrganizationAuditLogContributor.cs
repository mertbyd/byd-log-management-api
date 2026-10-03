using Microsoft.Extensions.DependencyInjection;
using Byd.CoreExtensions.Interfaces;
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
        var currentOrganization = context.ServiceProvider.GetRequiredService<ICurrentOrganization>();
        context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationIdPropertyName, currentOrganization.Id);
        context.AuditInfo.SetProperty(AuditLoggingConsts.OrganizationNamePropertyName, currentOrganization.Name);
    }
}
