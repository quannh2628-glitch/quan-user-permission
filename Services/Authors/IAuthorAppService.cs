using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using ABPPermission.Services.Dtos.Authors;

namespace ABPPermission.Services.Authors;

public interface IAuthorAppService :
    ICrudAppService< //Defines CRUD methods
        AuthorDto, //Used to show authors
        Guid, //Primary key of the author entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateAuthorDto>, //Used to create/update an author
    IAuthorLookupAppService //Used to lookup authors for dropdown
{
}
