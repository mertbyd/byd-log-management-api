using FluentValidation;
using Byd.LogManagement.Consts;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.ExceptionCodes;

namespace Byd.LogManagement.Validators.Notifications;

/// <summary>
/// Firebase projesi create ve update isteklerinin ortak sekil kurallarini tanimlar.
/// </summary>
// islevi: Proje adi zorunluluk/uzunluk ve kod zorunluluk hatalarini veritabanina gitmeden reddeder.
// sistemdeki gorevi: Create ve update validator'larinin kararli hata kodu kullanan ortak tabanidir.
public abstract class FirebaseProjectInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FirebaseProjectInputDto
{
    protected FirebaseProjectInputDtoValidatorBase()
    {
        RuleFor(input => input.Name)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.FirebaseProjectNameRequired)
            .MaximumLength(NotificationConsts.MaxLookupNameLength)
            .WithErrorCode(NotificationExceptionCodes.FirebaseProjectNameTooLong);
        RuleFor(input => input.Key)
            .NotEmpty().WithErrorCode(NotificationExceptionCodes.FirebaseProjectKeyRequired)
            .MaximumLength(NotificationConsts.MaxFirebaseProjectKeyLength)
            .WithErrorCode(NotificationExceptionCodes.FirebaseProjectKeyTooLong)
            .Matches(NotificationConsts.FirebaseProjectKeyPattern)
            .WithErrorCode(NotificationExceptionCodes.FirebaseProjectKeyInvalid);
    }
}
