using Byd.LogManagement.Dtos.Notifications;

namespace Byd.LogManagement.Validators.Notifications;

/// <summary>
/// FirebaseProject create istegini ortak alan kurallariyla dogrular.
/// </summary>
// islevi: CreateFirebaseProjectDto icin FluentValidation kurallarini etkinlestirir.
// sistemdeki gorevi: Create endpointinin public girdi validator'idir.
public sealed class CreateFirebaseProjectDtoValidator
    : FirebaseProjectInputDtoValidatorBase<CreateFirebaseProjectDto>
{
}
