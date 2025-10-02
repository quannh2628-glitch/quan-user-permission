using Volo.Abp.Application.Services;
using ABPPermission.Localization;

namespace ABPPermission.Services;

/* Inherit your application services from this class. */
public abstract class ABPPermissionAppService : ApplicationService
{
    protected ABPPermissionAppService()
    {
        LocalizationResource = typeof(ABPPermissionResource);
    }
}