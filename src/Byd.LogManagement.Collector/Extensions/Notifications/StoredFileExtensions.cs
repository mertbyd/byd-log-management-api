using System.Collections.Generic;
using System.Linq;
using Byd.FileManagement.Dtos.StoredFiles;
using Byd.LogManagement.Models.Notifications;

namespace Byd.LogManagement.Extensions.Notifications;

/// <summary>
/// Dosya modulu secret cevaplarini bildirim domain modeline donusturur.
/// </summary>
// islevi: Dis modulun StoredFileWithContentDto alanlarini provider-bagimsiz secret modeline tasir.
// sistemdeki gorevi: Mobil gondericiyi elle alan tasimadan uzak tutan inbound-contract donusum sahibidir.
public static class StoredFileExtensions
{
    // Secret dosyalarinin secim icin gereken kimlik, zaman ve byte alanlarini tasir.
    public static List<FirebaseProjectSecretModel> ToFirebaseProjectSecrets(
        this IEnumerable<StoredFileWithContentDto> files)
        => [.. files.Select(file => new FirebaseProjectSecretModel
        {
            Id = file.Id,
            CreationTime = file.CreationTime,
            Data = file.Data
        })];
}
