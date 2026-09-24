using Byd.CoreExtensions.Interfaces;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Interfaces.Notifications;

/// <summary>
/// Firebase projesi lookup kaydi icin extended repository sozlesmesini tanimlar.
/// </summary>
// islevi: CRUD, filtreli liste ve sayim yeteneklerini tabandan alir; ek sorgu tanimlamaz.
// sistemdeki gorevi: Firebase projesi kullanim senaryolari ile EF Core uygulamasi arasindaki veri erisim siniridir.
public interface IFirebaseProjectRepository : IExtendedRepository<FirebaseProject>
{
}
