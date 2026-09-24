using System;

namespace Byd.LogManagement.Etos.Notifications;

/// <summary>
/// Bildirimin gonderilecegi tek bir aliciyi tasir.
/// </summary>
// islevi: Alicinin kimligini ve bildirim anindaki kullanici adini tasir.
// sistemdeki gorevi: Fan-out'un girdisidir; listedeki her oge kuyruga bir satir olarak yazilir.
public class NotificationRecipientEto
{
    // Bildirimin gonderilecegi kullanicinin kimligi.
    public Guid RecipientUserId { get; set; }

    // Bildirim anindaki alici kullanici adi.
    public string RecipientUserName { get; set; } = default!;
}
