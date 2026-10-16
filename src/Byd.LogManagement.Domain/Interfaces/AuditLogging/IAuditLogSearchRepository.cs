using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.AuditLogging;

namespace Byd.LogManagement.Interfaces.AuditLogging;

/// <summary>
/// Audit kayitlarinin Elasticsearch indeksi icin repository sozlesmesini tanimlar.
/// </summary>
// islevi: AuditLog belgelerini yazma, okuma ve DataTable ile listeleme yeteneklerini core'un arama tabanindan alir.
// sistemdeki gorevi: Audit store'un arama indeksine yazdigi sinirdir; veritabani tarafi ABP'nin IAuditLogRepository'sinde kalir.
public interface IAuditLogSearchRepository : IElasticsearchRepository<AuditLog, Guid>
{
}
