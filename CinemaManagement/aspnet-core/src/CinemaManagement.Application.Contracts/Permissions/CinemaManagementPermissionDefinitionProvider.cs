using CinemaManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CinemaManagement.Permissions;

public class CinemaManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(CinemaManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CinemaManagementPermissions.MyPermission1, L("Permission:MyPermission1"));

        var CinemaGroup = context.AddGroup(CinemaManagementPermissions.GroupName, L("Permission:CinemaManagement"));

        var screenTypePermission = CinemaGroup.AddPermission(CinemaManagementPermissions.screenTypes.Default, L("Permission:ScreenTypes"));
        screenTypePermission.AddChild(CinemaManagementPermissions.screenTypes.Create, L("Permission:ScreenTypes.Create"));
        screenTypePermission.AddChild(CinemaManagementPermissions.screenTypes.Edit, L("Permission:ScreenTypes.Edit"));
        screenTypePermission.AddChild(CinemaManagementPermissions.screenTypes.Delete, L("Permission:ScreenTypes.Delete"));

        var genrePermission = CinemaGroup.AddPermission(CinemaManagementPermissions.Genres.Default, L("Permission:Genres"));
        genrePermission.AddChild(CinemaManagementPermissions.Genres.Create, L("Permission:Genres.Create"));
        genrePermission.AddChild(CinemaManagementPermissions.Genres.Edit, L("Permission:Genres.Edit"));
        genrePermission.AddChild(CinemaManagementPermissions.Genres.Delete, L("Permission:Genres.Delete"));

        var countriePermission = CinemaGroup.AddPermission(CinemaManagementPermissions.Countries.Default, L("Permission:Countries"));
        countriePermission.AddChild(CinemaManagementPermissions.Countries.Create, L("Permission:Countries.Create"));
        countriePermission.AddChild(CinemaManagementPermissions.Countries.Edit, L("Permission:Countries.Edit"));
        countriePermission.AddChild(CinemaManagementPermissions.Countries.Delete, L("Permission:Countries.Delete"));

        var moviePermission = CinemaGroup.AddPermission(CinemaManagementPermissions.Movies.Default, L("Permission:Movies"));
        moviePermission.AddChild(CinemaManagementPermissions.Movies.Create, L("Permission:Movies.Create"));
        moviePermission.AddChild(CinemaManagementPermissions.Movies.Edit, L("Permission:Movies.Edit"));
        moviePermission.AddChild(CinemaManagementPermissions.Movies.Delete, L("Permission:Movies.Delete"));


        var ticketPermission = CinemaGroup.AddPermission(CinemaManagementPermissions.Tickets.Default, L("Permission:Tickets"));
        ticketPermission.AddChild(CinemaManagementPermissions.Tickets.Create, L("Permission:Tickets.Create"));
        ticketPermission.AddChild(CinemaManagementPermissions.Tickets.Edit, L("Permission:Tickets.Edit"));
        ticketPermission.AddChild(CinemaManagementPermissions.Tickets.Delete, L("Permission:Tickets.Delete"));


        var cinemazoomPermission = CinemaGroup.AddPermission(CinemaManagementPermissions.CinemaZooms.Default, L("Permission:CinemaZooms"));
        cinemazoomPermission.AddChild(CinemaManagementPermissions.CinemaZooms.Create, L("Permission:CinemaZooms.Create"));
        cinemazoomPermission.AddChild(CinemaManagementPermissions.CinemaZooms.Edit, L("Permission:CinemaZooms.Edit"));
        cinemazoomPermission.AddChild(CinemaManagementPermissions.CinemaZooms.Delete, L("Permission:CinemaZooms.Delete"));

        var showtimePermission = CinemaGroup.AddPermission(CinemaManagementPermissions.ShowTimes.Default, L("Permission:ShowTimes"));
        showtimePermission.AddChild(CinemaManagementPermissions.ShowTimes.Create, L("Permission:ShowTimes.Create"));
        showtimePermission.AddChild(CinemaManagementPermissions.ShowTimes.Edit, L("Permission:ShowTimes.Edit"));
        showtimePermission.AddChild(CinemaManagementPermissions.ShowTimes.Delete, L("Permission:ShowTimes.Delete")); 
        
        var SellersPermission = CinemaGroup.AddPermission(CinemaManagementPermissions.Sellers.Default, L("Permission:Sellers"));

        
        var DataManagementPermission = CinemaGroup.AddPermission(CinemaManagementPermissions.DataManagement.Default, L("Permission:DataManagement"));
        DataManagementPermission.AddChild(CinemaManagementPermissions.DataManagement.Create, L("Permission:DataManagement.Create"));
     
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CinemaManagementResource>(name);
    }
}
