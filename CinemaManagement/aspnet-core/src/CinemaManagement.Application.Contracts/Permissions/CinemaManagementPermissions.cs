namespace CinemaManagement.Permissions;

public static class CinemaManagementPermissions
{
    public const string GroupName = "CinemaManagement";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class screenTypes
    {
        public const string Default = GroupName + ".screenTypes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Genres
    {
        public const string Default = GroupName + ".Genres";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Countries
    {
        public const string Default = GroupName + ".Countries";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Movies
    {
        public const string Default = GroupName + ".Movies";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class CinemaZooms
    {
        public const string Default = GroupName + ".CinemaZooms";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class ShowTimes
    {
        public const string Default = GroupName + ".ShowTimes";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }


    public static class Tickets
    {
        public const string Default = GroupName + ".Tickets";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Sellers
    {
        public const string Default = GroupName + ".Sellers";

    }

    public static class DataManagement
    {
        public const string Default = GroupName + ".DataManagement";
        public const string Create = Default + ".Create";
    }
}
