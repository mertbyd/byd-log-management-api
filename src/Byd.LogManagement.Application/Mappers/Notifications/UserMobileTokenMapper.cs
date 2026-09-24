using System;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Models.Notifications;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.Notifications;

/// <summary>
/// Mobil token kayit girdisinin domain model eslemesini tanimlar.
/// </summary>
// islevi: Kayit istegini, cozulmus proje kimligi ve oturumdaki kullaniciyla Mapperly tarafindan uretilen kodla modele
// donusturur.
// sistemdeki gorevi: UserMobileToken katmanlari arasindaki alan tasimanin tek sahibidir.
[Mapper]
public partial class UserMobileTokenMapper
{
    public partial UserMobileTokenRegisterModel Map(
        RegisterUserMobileTokenDto source,
        Guid firebaseProjectId,
        Guid userId);
}
