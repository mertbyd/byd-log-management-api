using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Mobil gonderim gunlugu kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: Ekleme, filtreli liste ve sayim yeteneklerini tabandan alir; ek sorgu tanimlamaz.
// sistemdeki gorevi: Mobil gonderim sonuclarinin yazildigi veri erisim siniridir.
public interface IMobileNotificationRepository : IExtendedRepository<MobileNotification>
{
}
