using System;


namespace TaskHub
{
    [Flags]
    public enum ApplicationPage
    {
        Home = 0,
        DataGrid,
        NewTask,
        Login,
        Register,
        LoginPage
    }
}
