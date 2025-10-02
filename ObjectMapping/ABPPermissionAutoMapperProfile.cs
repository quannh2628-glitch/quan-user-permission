using AutoMapper;
using ABPPermission.Entities.Books;
using ABPPermission.Services.Dtos.Books;

namespace ABPPermission.ObjectMapping;

public class ABPPermissionAutoMapperProfile : Profile
{
    public ABPPermissionAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookDto, CreateUpdateBookDto>();
        /* Create your AutoMapper object mappings here */
    }
}