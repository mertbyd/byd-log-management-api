using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.FileManagement.Etos.StoredFiles;
using Byd.FileManagement.Services.StoredFiles;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Interfaces.Notifications;
using Byd.LogManagement.Managers.Notifications;
using Byd.LogManagement.Mappers.Notifications;
using Volo.Abp.EventBus.Distributed;

namespace Byd.LogManagement.Services.Notifications;

/// <summary>
/// Firebase projelerinin CRUD ve FCM secret yukleme orkestrasyonunu yurutur.
/// </summary>
// islevi: Girdi dogrulamasini, Manager mutasyonunu ve repository kaydini duz akisla birlestirir; secret dosyasini
// dosya modulune yukler.
// sistemdeki gorevi: Controller ile FirebaseProject Manager/Repository zinciri arasindaki tek public giris noktasidir.
public class FirebaseProjectAppService : LogManagementAppService, IFirebaseProjectAppService
{
    private static readonly FirebaseProjectMapper Mapper = new();

    private FirebaseProjectManager Manager =>
        LazyGetRequiredService<FirebaseProjectManager>();
    private IFirebaseProjectRepository Repository =>
        LazyGetRequiredService<IFirebaseProjectRepository>();
    private IValidator<CreateFirebaseProjectDto> CreateValidator =>
        LazyGetRequiredService<IValidator<CreateFirebaseProjectDto>>();
    private IValidator<UpdateFirebaseProjectDto> UpdateValidator =>
        LazyGetRequiredService<IValidator<UpdateFirebaseProjectDto>>();
    private IValidator<UploadFirebaseProjectSecretDto> UploadSecretValidator =>
        LazyGetRequiredService<IValidator<UploadFirebaseProjectSecretDto>>();
    private IStoredFileAppService StoredFileAppService =>
        LazyGetRequiredService<IStoredFileAppService>();
    private IDistributedEventBus DistributedEventBus =>
        LazyGetRequiredService<IDistributedEventBus>();

    // Kimligi verilen projeyi Core Manager guard'i uzerinden getirir.
    public virtual async Task<FirebaseProjectDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map((await Manager.TryGetByIdAsync(
            id,
            throwIfNotExists: true,
            cancellationToken))!);

    // Istegin tenant'indaki projeleri repository'den getirir.
    public virtual async Task<List<FirebaseProjectDto>> GetListAsync(CancellationToken cancellationToken = default)
        => Mapper.Map(await Repository.GetListAsync(includeDetails: false, cancellationToken: cancellationToken));

    // Create girdisini dogrular, Manager ile entity kurar ve tek UOW icinde kaydeder.
    // Kapsam istekte gelen tenant'tir: tekillik kontrolleri ve kayit o tenant icinde yapilir.
    // TODO: Auth modulu bagalandiginda burdaki tanet işinin nasıl yürütüleceği mentörlere sorulacak tanet dışardan mı verilmeli  yoksa o an login olan kullanıcının tanetına mı göre olmalı
    public virtual async Task<FirebaseProjectDto> CreateAsync(
        CreateFirebaseProjectDto input,
        CancellationToken cancellationToken = default)
    {
        await CreateValidator.ValidateAndThrowAsync(input, cancellationToken);
        // Tenant verilmediyse istegin kendi kapsami korunur; verildiyse o kapsama gecilir.
        using var tenant = CurrentTenant.Change(input.TenantId ?? CurrentTenant.Id);
        var model = Mapper.Map(input);
        var entity = await Manager.CreateAsync(model, cancellationToken);
        await Repository.InsertAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        return Mapper.Map(entity);
    }
    // Update girdisini dogrular, mevcut entity'yi Manager ile degistirir ve kaydeder.
    public virtual async Task<FirebaseProjectDto> UpdateAsync(
        Guid id,
        UpdateFirebaseProjectDto input,
        CancellationToken cancellationToken = default)
    {
        await UpdateValidator.ValidateAndThrowAsync(input, cancellationToken);
        var entity = (await Manager.TryGetByIdAsync(
            id,
            throwIfNotExists: true,
            cancellationToken))!;
        var model = Mapper.Map(input);
        await Manager.UpdateAsync(entity, model, cancellationToken);
        await Repository.UpdateAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        return Mapper.Map(entity);
    }
    // Kimligi verilen projeyi siler; secret dosyalarinin silinmesi icin dosya modulune ayni UOW icinde haber verir.
    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = (await Manager.TryGetByIdAsync(
            id,
            throwIfNotExists: true,
            cancellationToken))!;
        await Repository.DeleteAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        await DistributedEventBus.PublishAsync(new ExternalRecordsDeletedEto
        {
            TenantId = entity.TenantId,
            ExternalIds = [entity.Id]
        });
    }
    // Secret girdisini dogrular, yeni dosyayi yukler ve projenin onceki secret dosyalarini siler.
    // Yeni dosya once yuklenir: yukleme basarisiz olursa eski secret yerinde kalir.
    // Kapsam istekte gelen tenant'tir: proje o tenant'ta aranir ve dosya o tenant'a yazilir.
    // TODO: Auth modulu bagalandiginda tenant istekten alinmayacak, kullanicinin token'indan okunacak.
    public virtual async Task UploadSecretAsync(
        Guid id,
        UploadFirebaseProjectSecretDto input,
        CancellationToken cancellationToken = default)
    {
        await UploadSecretValidator.ValidateAndThrowAsync(input, cancellationToken);
        // Tenant verilmediyse istegin kendi kapsami korunur; verildiyse o kapsama gecilir.
        using var tenant = CurrentTenant.Change(input.TenantId ?? CurrentTenant.Id);
        var entity = (await Manager.TryGetByIdAsync(
            id,
            throwIfNotExists: true,
            cancellationToken))!;
        var category = await Manager.GetSecretCategoryAsync(cancellationToken);
        var previousFiles = await StoredFileAppService.GetListByCategoryAsync(entity.Id, category.Id);
        await StoredFileAppService.UploadAsync(Mapper.Map(input, entity.Id, category.Id));
        foreach (var file in previousFiles)
            await StoredFileAppService.DeleteAsync(file.Id);
    }
}
