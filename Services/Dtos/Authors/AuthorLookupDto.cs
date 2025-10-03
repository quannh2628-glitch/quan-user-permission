using Volo.Abp.Application.Dtos;

namespace ABPPermission.Services.Dtos.Authors;

public class AuthorLookupDto : EntityDto<Guid>
{
    public string FullName { get; set; }
}
