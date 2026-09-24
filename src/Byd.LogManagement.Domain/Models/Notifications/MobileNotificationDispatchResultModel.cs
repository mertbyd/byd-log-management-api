using System.Collections.Generic;
using Byd.LogManagement.Entities.Notifications;

namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Bir bildirim satirinin mobil gonderim sonuclarini tasir.
/// </summary>
// islevi: Bildirim satirini hedef bazli sonuclariyla proje grubu sonundaki toplu kayda tasir.
// sistemdeki gorevi: Dispatcher'in provider cagrilari ile Manager'in tek bulk repository yazisi arasindaki modeldir.
public sealed class MobileNotificationDispatchResultModel
{
    public Notification Notification { get; set; } = default!;
    public IReadOnlyCollection<MobileDeliveryResultModel> Results { get; set; } = [];
}
