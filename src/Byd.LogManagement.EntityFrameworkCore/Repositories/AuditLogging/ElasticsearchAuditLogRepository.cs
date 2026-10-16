using System;
using Byd.CoreExtensions.Repositories;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Interfaces.AuditLogging;
using Volo.Abp.AuditLogging;
using Volo.Abp.DependencyInjection;

namespace Byd.LogManagement.EntityFrameworkCore.Repositories.AuditLogging;

/// <summary>
/// IAuditLogSearchRepository sozlesmesini audit kayitlarinin Elasticsearch indeksine baglar.
/// </summary>
// islevi: AuditLog belgelerini AuditLoggingConsts.IndexName indeksinde tutar.
// sistemdeki gorevi: IAuditLogSearchRepository sozlesmesinin ElasticsearchRepository tabanli uygulamasidir.
// mirastan gelen: indeks yonetimi, yazma, guncelleme, silme, okuma, filtreli liste, arama ve sayim yuzeyleri; istemci ayarlardan
// gelir, okumalar tenant ile sinirlanir.
// burada yazilan: yalniz indeks adi; audit kaydi tabanin disinda sorgu istemez.
[ExposeServices(typeof(IAuditLogSearchRepository))]
public class ElasticsearchAuditLogRepository
    : ElasticsearchRepository<AuditLog, Guid>,
        IAuditLogSearchRepository,
        ITransientDependency
{
    protected override string IndexName => AuditLoggingConsts.IndexName;
}
