using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ABPPermission.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using ABPPermission.Entities.Authors;
using ABPPermission.Services.Dtos.Authors;

namespace ABPPermission.Services.Authors;

[Authorize(ABPPermissionPermissions.Authors.Default)]
public class AuthorAppService : CrudAppService<Author, AuthorDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateAuthorDto, CreateUpdateAuthorDto>, IAuthorAppService
{
    private readonly IRepository<Author, Guid> _repository;

    public AuthorAppService(IRepository<Author, Guid> repository) : base(repository)
    {
        _repository = repository;
    }

    protected override IQueryable<Author> ApplyDefaultSorting(IQueryable<Author> query)
    {
        return query.OrderBy(x => x.FullName);
    }

    [Authorize(ABPPermissionPermissions.Authors.Create)]
    public override async Task<AuthorDto> CreateAsync(CreateUpdateAuthorDto input)
    {
        return await base.CreateAsync(input);
    }

    [Authorize(ABPPermissionPermissions.Authors.Edit)]
    public override async Task<AuthorDto> UpdateAsync(Guid id, CreateUpdateAuthorDto input)
    {
        return await base.UpdateAsync(id, input);
    }

    [Authorize(ABPPermissionPermissions.Authors.Delete)]
    public override async Task DeleteAsync(Guid id)
    {
        await base.DeleteAsync(id);
    }

    public async Task<List<AuthorLookupDto>> GetLookupAsync()
    {
        var queryable = await _repository.GetQueryableAsync();
        var authors = await AsyncExecuter.ToListAsync(queryable.OrderBy(x => x.FullName));
        
        return ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors);
    }
}
