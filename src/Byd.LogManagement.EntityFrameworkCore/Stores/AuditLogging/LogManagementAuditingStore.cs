using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Interfaces.AuditLogging;
using Byd.LogManagement.Settings;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace Byd.LogManagement.EntityFrameworkCore.Stores.AuditLogging;

/// <summary>
/// Audit kaydini ayarin sectigi yere, veritabanina, Elasticsearch indeksine ya da ikisine yazar.
/// </summary>
// islevi: Kaydi bir kez donusturur; veritabanina ABP'nin repository'siyle, indekse IAuditLogSearchRepository ile ayni kimlikle yazar.
// sistemdeki gorevi: ABP'nin audit store'unun yerine gecer; yazma yeri LogManagement.AuditLogSaveMode ayarindan okunur, varsayilan veritabanidir.
[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IAuditingStore))]
public class LogManagementAuditingStore : AuditingStore
{
    private readonly IAuditLogSearchRepository _auditLogSearchRepository;
    private readonly ISettingProvider _settingProvider;

    public LogManagementAuditingStore(
        IAuditLogRepository auditLogRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IOptions<AbpAuditingOptions> options,
        IAuditLogInfoToAuditLogConverter converter,
        IAuditLogSearchRepository auditLogSearchRepository,
        ISettingProvider settingProvider)
        : base(auditLogRepository, unitOfWorkManager, options, converter)
    {
        _auditLogSearchRepository = auditLogSearchRepository;
        _settingProvider = settingProvider;
    }

    // Kaydi donusturur ve ayarin sectigi yerlere yazar; taninmayan deger veritabanina yazmaya duser.
    protected override async Task SaveLogAsync(AuditLogInfo auditInfo)
    {
        var saveMode = await _settingProvider.GetOrNullAsync(LogManagementSettings.AuditLogSaveMode);
        var auditLog = await Converter.ConvertAsync(auditInfo);
        if (saveMode != AuditLoggingConsts.ElasticsearchSaveMode)
            await SaveToDatabaseAsync(auditLog);
        if (saveMode is AuditLoggingConsts.ElasticsearchSaveMode or AuditLoggingConsts.BothSaveMode)
            await _auditLogSearchRepository.InsertAsync(auditLog);
    }

    // Kaydi ABP'nin kendi store'u gibi ayri bir unit of work icinde veritabanina yazar.
    private async Task SaveToDatabaseAsync(AuditLog auditLog, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = UnitOfWorkManager.Begin(requiresNew: true);
        await AuditLogRepository.InsertAsync(auditLog, cancellationToken: cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
    }
}
