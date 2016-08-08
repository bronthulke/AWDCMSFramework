using System;

namespace AWDCMSFramework.Domain.Interfaces
{
    public interface ILogInfo
    {
        string CreateUser { get; set; }
        DateTime CreateDate { get; set; }
        string UpdateUser { get; set; }
        DateTime UpdateDate { get; set; }

    }
}
