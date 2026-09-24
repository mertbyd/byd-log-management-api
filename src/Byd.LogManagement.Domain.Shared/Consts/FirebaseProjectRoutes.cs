namespace Byd.LogManagement.Consts;

/// <summary>
/// Firebase projesi HTTP rotalarini tanimlar.
/// </summary>
// islevi: FirebaseProject CRUD endpointlerinin kararli rota sozlesmesini tek yerde tutar.
// sistemdeki gorevi: Controller ile istemcilerin ayni derleme guvenli route degerlerini kullanmasini saglar.
public static class FirebaseProjectRoutes
{
    // Firebase projesi endpoint ailesinin kok rotasidir.
    public const string Root = "api/log-management/firebase-projects";

    // Kimlik alan CRUD endpointlerinin rota kalibidir.
    public const string ById = "{id:guid}";

    // Projenin FCM secret dosyasini yukleyen endpointin rota kalibidir.
    public const string Secret = "{id:guid}/secret";
}
