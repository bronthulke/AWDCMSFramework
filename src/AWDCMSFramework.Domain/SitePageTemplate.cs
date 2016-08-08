using System;
using AWDCMSFramework.Domain.Interfaces;

namespace AWDCMSFramework.Domain
{
    public class SitePageTemplate : ILogInfo, IEntity
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Html { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
