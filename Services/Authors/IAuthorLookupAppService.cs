using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using ABPPermission.Services.Dtos.Authors;

namespace ABPPermission.Services.Authors;

public interface IAuthorLookupAppService : IApplicationService
{
    Task<List<AuthorLookupDto>> GetLookupAsync();
}
