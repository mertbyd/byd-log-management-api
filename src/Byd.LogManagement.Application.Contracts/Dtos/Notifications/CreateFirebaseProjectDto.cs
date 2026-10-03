namespace Byd.LogManagement.Dtos.Notifications;

/// <summary>
/// Yeni Firebase projesi olusturma istegini tanimlar.
/// </summary>
// islevi: FirebaseProject create endpointinin public girdi tipidir; proje istegi yapan kullanicinin tenant'inda acilir.
// sistemdeki gorevi: Kimlik ve audit alanlarini sunucuda birakip degistirilebilir alanlari kabul eder.
public sealed class CreateFirebaseProjectDto : FirebaseProjectInputDto
{
}
