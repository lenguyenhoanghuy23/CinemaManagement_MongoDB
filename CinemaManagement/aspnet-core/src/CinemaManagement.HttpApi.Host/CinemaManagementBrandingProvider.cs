using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CinemaManagement;

[Dependency(ReplaceServices = true)]
public class CinemaManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CinemaManagement";
}
