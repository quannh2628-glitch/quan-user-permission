using AutoMapper;
using ABPPermission.Entities.Books;
using ABPPermission.Services.Dtos.Books;
using ABPPermission.Entities.Authors;
using ABPPermission.Services.Dtos.Authors;
using ABPPermission.Entities.Customers;
using ABPPermission.Services.Dtos.Customers;

namespace ABPPermission.ObjectMapping;

public class ABPPermissionAutoMapperProfile : Profile
{
    public ABPPermissionAutoMapperProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        CreateMap<BookDto, CreateUpdateBookDto>();
        
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateUpdateAuthorDto, Author>();
        CreateMap<AuthorDto, CreateUpdateAuthorDto>();
        CreateMap<Author, AuthorLookupDto>();

        CreateMap<AppCustomer, CustomerDto>();
        CreateMap<CreateUpdateCustomerDto, AppCustomer>();
        CreateMap<CustomerDto, CreateUpdateCustomerDto>();
        /* Create your AutoMapper object mappings here */
    }
}