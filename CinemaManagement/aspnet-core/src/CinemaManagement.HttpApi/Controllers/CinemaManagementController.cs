using CinemaManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CinemaManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CinemaManagementController : AbpControllerBase
{
    protected CinemaManagementController()
    {
        LocalizationResource = typeof(CinemaManagementResource);
    }
}
