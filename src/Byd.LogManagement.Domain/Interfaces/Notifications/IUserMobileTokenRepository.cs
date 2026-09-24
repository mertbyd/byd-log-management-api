using System;
using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// UserMobileToken icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: UserMobileToken ekleme, filtreli liste ve sayim yeteneklerini tabandan alir.
// sistemdeki gorevi: Mobil token kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface IUserMobileTokenRepository : IExtendedRepository<UserMobileToken>
{
}
