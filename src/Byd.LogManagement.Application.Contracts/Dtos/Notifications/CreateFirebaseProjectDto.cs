using System;

namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Yeni Firebase projesi olusturma istegini tanimlar.
/// </summary>
// islevi: FirebaseProject create endpointinin public girdi tipidir; projenin acilacagi tenant'i da tasir.
// sistemdeki gorevi: Kimlik ve audit alanlarini sunucuda birakip degistirilebilir alanlari kabul eder.
public sealed class CreateFirebaseProjectDto : FirebaseProjectInputDto
{
    // Projenin acilacagi tenant; host projeleri icin bos birakilir.
    // TODO: Auth modulu bagalandiginda kaldirilacak; burdaki davranış mentöre danışılacak nasıl olması gerektiği önce auth
    public Guid? TenantId { get; set; }
}
