using System;

namespace MainActivity
{
    interface ILogs
    {
        string Username { get; set; }
        string Log { get; set; }
        string LogDate { get; set; }
    }
}
