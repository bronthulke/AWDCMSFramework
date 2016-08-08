using System;
using System.ComponentModel.DataAnnotations;
using AWDCMSFramework.Domain.Interfaces;

namespace AWDCMSFramework.Domain
{
    public class NewsArticle : ILogInfo, IEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Alias { get; set; }
        public bool IsLive { get; set; }
        public DateTime PublishDate { get; set; }
        public int? CategoryId { get; set; }
        public virtual NewsArticleCategory Category { get; set; }
        [MaxLength(256)]
        public string ImageURL { get; set; }
        [MaxLength(256)]
        public string ImageURLThumb { get; set; }
        [MaxLength(200)]
        public string ImageCaption { get; set; }
        public string Abstract { get; set; }
        public string ContentHTML { get; set; }
        public bool CopyToTheDogs { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
