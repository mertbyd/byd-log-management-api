using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.LogManagement.Dtos.Notifications;
using Volo.Abp.Application.Services;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Firebase projesi tam CRUD kullanim senaryolarini tanimlar.
/// </summary>
// islevi: FirebaseProject okuma, listeleme, olusturma, guncelleme ve silme kontratini acar.
// sistemdeki gorevi: Controller ile Application implementasyonu arasindaki public servis siniridir.
public interface IFirebaseProjectAppService : IApplicationService
{
    // Kimligi verilen Firebase projesini getirir.
    Task<FirebaseProjectDto> GetAsync(Guid id, CancellationToken cancellationToken = default);

    // Istegin tenant'indaki Firebase projelerini getirir.
    Task<List<FirebaseProjectDto>> GetListAsync(CancellationToken cancellationToken = default);

    // Dogrulanmis girdiden yeni Firebase projesi olusturur.
    Task<FirebaseProjectDto> CreateAsync(
        CreateFirebaseProjectDto input,
        CancellationToken cancellationToken = default);

    // Kimligi verilen Firebase projesinin degistirilebilir alanlarini gunceller.
    Task<FirebaseProjectDto> UpdateAsync(
        Guid id,
        UpdateFirebaseProjectDto input,
        CancellationToken cancellationToken = default);

    // Kimligi verilen Firebase projesini siler.
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // Kimligi verilen Firebase projesinin FCM secret dosyasini yenisiyle degistirir.
    Task UploadSecretAsync(
        Guid id,
        UploadFirebaseProjectSecretDto input,
        CancellationToken cancellationToken = default);
}
