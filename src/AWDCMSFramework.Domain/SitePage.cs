using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AWDCMSFramework.Domain.Interfaces;

namespace AWDCMSFramework.Domain
{
    public class SitePage : ILogInfo, IEntity
    {
        public int Id { get; set; }
        public virtual SitePage Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual ICollection<SitePage> Children { get; set; }
        [MaxLength(100)]
        public string Alias { get; set; }
        [MaxLength(160)]
        public string Name { get; set; }
        [MaxLength(160)]
        public string Title { get; set; }
        public virtual SitePageTemplate Template { get; set; }
        public int? SitePageTemplateId { get; set; }
        public bool IsLive { get; set; }
        public int SeqNum { get; set; }
        [MaxLength(170)]
        public string MetaDescription { get; set; }
        [MaxLength(300)]
        public string MetaKeywords { get; set; }
        [MaxLength(256)]
        public string RedirectURL { get; set; }
        [Required]
        public virtual ICollection<SitePageLayoutArea> LayoutAreas { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
