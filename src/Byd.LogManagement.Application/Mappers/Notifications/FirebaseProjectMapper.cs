using System;
using System.Collections.Generic;
using Byd.FileManagement.Dtos.StoredFiles;
using Byd.LogManagement.Dtos.Notifications;
using Byd.LogManagement.Entities.Notifications;
using Byd.LogManagement.Models.Notifications;
using Riok.Mapperly.Abstractions;

namespace Byd.LogManagement.Mappers.Notifications;

/// <summary>
/// FirebaseProject DTO, domain model ve entity eslemelerini tanimlar.
/// </summary>
// islevi: CRUD girdileri ile ciktilari ve secret yuklemesinin dosya modulu girdisini Mapperly tarafindan uretilen
// kodla donusturur.
// sistemdeki gorevi: FirebaseProject katmanlari arasindaki alan tasimanin tek sahibidir.
[Mapper]
public partial class FirebaseProjectMapper
{
    public partial FirebaseProjectCreateModel Map(CreateFirebaseProjectDto source);
    public partial FirebaseProjectUpdateModel Map(UpdateFirebaseProjectDto source);
    public partial FirebaseProjectDto Map(FirebaseProject source);
    public partial List<FirebaseProjectDto> Map(List<FirebaseProject> source);

    // Secret girdisini, projenin kimligi ve secret kategorisiyle dosya modulunun yukleme girdisine esler.
    public partial UploadStoredFileDto Map(UploadFirebaseProjectSecretDto source, Guid externalId, Guid categoryId);
}
