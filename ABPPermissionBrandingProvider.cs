using Microsoft.Extensions.Localization;
using ABPPermission.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ABPPermission;

[Dependency(ReplaceServices = true)]
public class ABPPermissionBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ABPPermissionResource> _localizer;

    public ABPPermissionBrandingProvider(IStringLocalizer<ABPPermissionResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}