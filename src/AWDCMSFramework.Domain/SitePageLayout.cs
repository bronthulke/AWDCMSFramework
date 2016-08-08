using System;
using System.ComponentModel.DataAnnotations;
using AWDCMSFramework.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace AWDCMSFramework.Domain
{
    public class SitePageLayoutArea : ILogInfo, IEntity
    {
        public int Id { get; set; }
        [ForeignKey("SitePage")]    // B. Squire - .Net Core upgrade change
        public int SitePageId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string HTML { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
