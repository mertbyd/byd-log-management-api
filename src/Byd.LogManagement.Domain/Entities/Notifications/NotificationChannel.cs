using System;
using Byd.CoreExtensions.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.LogManagement.Entities.Notifications;

/// <summary>
/// Bildirimlerin gonderildigi kanal kaydini tasir.
/// </summary>
// islevi: Kaydin adini ve is kodunu kalici olarak tutar.
// sistemdeki gorevi: Kurallari Manager'a birakan ince lookup persistence kabugudur. Notification buna baglanir.
public class NotificationChannel : CreationAuditedEntity<Guid>, ILookup
{
    // Kaydin kullaniciya gosterilen adidir.
    public string Name { get; internal set; } = default!;

    // Is tarafinin kodudur; NotificationConsts icindeki kodlarla eslesir.
    public int? Code { get; internal set; }

    // EF Core materializasyonu icin bos kurucudur.
    protected NotificationChannel()
    {
    }

    // Manager tarafindan uretilen kimlikle bos veri kabugunu kurar.
    internal NotificationChannel(Guid id)
        : base(id)
    {
    }

    // ILookup yazma yuzeyini Domain disina public entity mutasyonu acmadan uygular.
    string ILookup.Name
    {
        get => Name;
        set => Name = value;
    }

    // ILookup kod yazma yuzeyini Domain disina public entity mutasyonu acmadan uygular.
    int? ILookup.Code
    {
        get => Code;
        set => Code = value;
    }
}
