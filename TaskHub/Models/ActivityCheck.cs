using System;


namespace TaskHub
{
    [Flags]
    public enum ActivityCheck
    {
        active,
        inactive,
        urgent,
        finished,
        inProgress
    }
}
