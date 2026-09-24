using FluentValidation;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.ExceptionCodes;

namespace Byd.LogManagement.Validators.Notifications;

/// <summary>
/// FCM secret yukleme isteginin sekil kurallarini tanimlar.
/// </summary>
// islevi: Bos dosya adi ve bos icerikle gelen istegi dosya moduluna gitmeden reddeder.
// sistemdeki gorevi: Secret yukleme endpointinin public girdi validator'idir; boyut ve uzanti kurallari dosya modulundedir.
public sealed class UploadFirebaseProjectSecretDtoValidator : AbstractValidator<UploadFirebaseProjectSecretDto>
{
    public UploadFirebaseProjectSecretDtoValidator()
    {
        RuleFor(input => input.FileName)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.FirebaseSecretFileNameRequired);
        RuleFor(input => input.Data)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.FirebaseSecretDataRequired);
    }
}
