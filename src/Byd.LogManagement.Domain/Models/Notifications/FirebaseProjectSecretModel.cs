using System;

namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Firebase projesinin FCM servis hesabi icerigini tasir.
/// </summary>
// islevi: Dosya kimligini, yuklenme zamanini ve secret byte'larini domain kararina ve mobil gondericiye tasir.
// sistemdeki gorevi: StoredFile DTO'sunu Collector ve Domain katmanlarina sizdirmayan provider-bagimsiz modeldir.
public sealed class FirebaseProjectSecretModel
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public byte[] Data { get; set; } = default!;
}
