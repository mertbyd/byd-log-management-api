# Byd.LogManagement

Audit, etkinlik ve dis servis cagri kayitlarini toplayan, web ve mobil bildirim gonderen ABP DDD modulu. Kayitlar
`log_management` semasinda durur.

## Paketler

Tuketen projede her katman ayni adli katman paketini referans alir ve o katmanin modulunu `[DependsOn]` ile ekler:

| Paket | Modul |
|---|---|
| `Byd.LogManagement.Domain.Shared` | `LogManagementDomainSharedModule` |
| `Byd.LogManagement.Domain` | `LogManagementDomainModule` |
| `Byd.LogManagement.Application.Contracts` | `LogManagementApplicationContractsModule` |
| `Byd.LogManagement.Application` | `LogManagementApplicationModule` |
| `Byd.LogManagement.EntityFrameworkCore` | `LogManagementEntityFrameworkCoreModule` |
| `Byd.LogManagement.HttpApi` | `LogManagementHttpApiModule` |
| `Byd.LogManagement.HttpApi.Client` | `LogManagementHttpApiClientModule` |
| `Byd.LogManagement.Collector` | `LogManagementCollectorModule` |

Modul `Byd.CoreExtensions` ve `Byd.FileManagement` paketlerine baglidir; ikisinin host sozlesmesi de gecerlidir.

## Uretici ve merkezi taraf

- Uretici uygulama Application paketini kullanir: etkinlik icin `[TrackActivity]`, bildirim icin `INotificationLogger`,
  dis servis cagrisi icin `IExternalServiceLogger`. Kayitlar olay olarak merkeze gider.
- Dis servis kaydi cagrinin yapildigi tenant ve organizasyonu (kimlik ve ad) tasir; degerler cagri anindaki
  `ICurrentTenant` ve `ICurrentOrganization`'dan okunur. Arka plan iscisi cagridan once kapsami degistirmelidir.
- Merkezi log host'u ayrica Collector paketini ekler: olay handler'lari, bildirim hub'i ve ileri tarihli bildirim
  iscisi yalniz orada calisir.
- `[TrackActivity]` degerleri action'in dondurdugu kayittan okur; govde donmeyen action'da (silme) deger ve kayit kimligi ayni
  adli action girdisinden (ornegin `id`) gelir.
- Bildirim hub'i (`/signalr-hubs/log-management/notifications`) WebSocket ve SSE ile baglanir. Tarayici bu tasimalarda
  baslik gonderemedigi icin access token `access_token` sorgu parametresiyle gelir; Collector modulu bunu host'un JWT
  olaylarini bozmadan kendisi okur.
- Audit kaydinin yeri `LogManagement.AuditLogSaveMode` ayariyla secilir: `Database` (varsayilan), `Elasticsearch` ya da
  `Both`. Elasticsearch baglantisi core'un `CoreExtensions.Elasticsearch.*` ayarlarindan gelir; indeks
  `log_management_audit_logs`'tur.

## Host baglantisi

- Baglanti dizesi adi `Default`'tur.
- Pipeline'da `app.UseCoreExtensions()` ve `app.UseFileManagement()` cagrilarindan sonra
  `app.UseAuthorization()` ve `app.UseAuditing()` gelir.
