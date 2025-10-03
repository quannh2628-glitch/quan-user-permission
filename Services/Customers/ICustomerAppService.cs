using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using ABPPermission.Services.Dtos.Customers;

namespace ABPPermission.Services.Customers;

public interface ICustomerAppService : IApplicationService
{
    Task<PagedResultDto<CustomerDto>> GetListAsync(GetCustomerListDto input);
    
    Task<CustomerDto> GetAsync(Guid id);
    
    Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input);
    
    Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input);
    
    Task DeleteAsync(Guid id);
    
    Task<InitCustomersResultDto> InitializeCustomersAsync(InitializeCustomersDto input);
}
