namespace Byd.LogManagement.Dtos.ExternalServiceLogs;

/// <summary>
/// Kaydedilecek dis servis cagrisinin is tanimini tasir.
/// </summary>
// islevi: Cagrilan saglayici, uc ve istek tipini tek degismez degerde toplar.
// sistemdeki gorevi: Bir cagrinin is anlamini yalniz cagiran bildigi icin sarmalayiciya verilen tek girdidir;
// degismez oldugu icin saglayici sinifinda bir kez tanimlanip her cagrida yeniden kurulmadan kullanilabilir.
public sealed record ExternalServiceCall(string ProviderName, string MethodName, string RequestType);
