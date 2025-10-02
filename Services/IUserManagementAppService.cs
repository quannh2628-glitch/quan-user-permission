using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using ABPPermission.Services.Dtos;

namespace ABPPermission.Services;

public interface IUserManagementAppService : IApplicationService
{
	Task<InitUsersResultDto> InitializeHundredUsersAsync();
	Task<FillDobResultDto> FillDateOfBirthForNullAsync();
}
