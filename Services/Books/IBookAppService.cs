using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using ABPPermission.Services.Dtos.Books;
using ABPPermission.Entities.Books;

namespace ABPPermission.Services.Books;

public interface IBookAppService :
    ICrudAppService< //Defines CRUD methods
        BookDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateBookDto> //Used to create/update a book
{

}