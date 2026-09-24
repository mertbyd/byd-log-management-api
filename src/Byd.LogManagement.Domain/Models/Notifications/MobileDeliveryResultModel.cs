namespace Byd.LogManagement.Models.Notifications;

/// <summary>
/// Tek bir mobil hedefe gonderim denemesinin sonucunu tasir.
/// </summary>
// islevi: Hedefi, basariyi, FCM kodunu, FCM aciklamasini, sureyi ve hedefin artik gecersiz olup olmadigini tasir.
// sistemdeki gorevi: FCM gondericisinin urettigi, Manager'in gunluge yazdigi ve gecersiz token temizligine kullandigi
// saglayicidan bagimsiz sonuc tipidir; FCM hata kodu gondericide bu bayraga cevrilir.
public sealed class MobileDeliveryResultModel
{
    public string Target { get; init; } = default!;
    public bool IsSuccess { get; init; }
    public string? StatusCode { get; init; }
    public string? StatusMessage { get; init; }
    public double ExecutionDuration { get; init; }
    public bool IsTargetInvalid { get; init; }
}
