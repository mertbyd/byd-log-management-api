using FluentValidation;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.ExceptionCodes;

namespace Byd.LogManagement.Validators.Notifications;

/// <summary>
/// Mobil token kayit isteginin sekil kurallarini tanimlar.
/// </summary>
// islevi: Proje anahtari, token ve dil kodu alanlarinin zorunluluk ve uzunluk kontrollerini veritabanina gitmeden reddeder.
// sistemdeki gorevi: Mobil token kayit endpointinin public girdi validator'idir.
public sealed class RegisterUserMobileTokenDtoValidator : AbstractValidator<RegisterUserMobileTokenDto>
{
    public RegisterUserMobileTokenDtoValidator()
    {
        RuleFor(input => input.FirebaseProjectKey)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.FirebaseProjectKeyRequired)
            .MaximumLength(NotificationConsts.MaxFirebaseProjectKeyLength)
            .WithErrorCode(NotificationExceptionCodes.FirebaseProjectKeyTooLong);
        RuleFor(input => input.Token)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.DeviceTokenRequired)
            .MaximumLength(NotificationConsts.MaxDeviceTokenLength)
            .WithErrorCode(NotificationExceptionCodes.DeviceTokenTooLong);
        // Dil kodu zorunlu degildir; verilmediginde gonderim istegin diline duser.
        RuleFor(input => input.LanguageCode)
            .MaximumLength(NotificationConsts.MaxLanguageCodeLength)
            .WithErrorCode(NotificationExceptionCodes.LanguageCodeTooLong);
    }
}
