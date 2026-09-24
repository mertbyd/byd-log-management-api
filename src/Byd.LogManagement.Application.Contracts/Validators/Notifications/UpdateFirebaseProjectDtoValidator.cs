using Byd.LogManagement.Dtos.Notifications;

namespace Byd.LogManagement.Validators.Notifications;

/// <summary>
/// FirebaseProject update istegini ortak alan kurallariyla dogrular.
/// </summary>
// islevi: UpdateFirebaseProjectDto icin FluentValidation kurallarini etkinlestirir.
// sistemdeki gorevi: Update endpointinin public girdi validator'idir.
public sealed class UpdateFirebaseProjectDtoValidator
    : FirebaseProjectInputDtoValidatorBase<UpdateFirebaseProjectDto>
{
}
