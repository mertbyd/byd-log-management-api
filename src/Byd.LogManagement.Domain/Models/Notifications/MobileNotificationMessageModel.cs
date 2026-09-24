using System.Collections.Generic;

namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Mobil saglayiciya gonderilecek provider-bagimsiz mesaji tanimlar.
/// </summary>
// islevi: Baslik, govde, hedefler ve hedeflerin topic olup olmadigi bilgisini tek send girdisinde tasir.
// sistemdeki gorevi: Dispatcher ile mobil sender arasindaki ortak token/topic mesaj sozlesmesidir.
public sealed class MobileNotificationMessageModel
{
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public IReadOnlyCollection<string> Targets { get; set; } = [];
    public bool IsTopic { get; set; }
}
