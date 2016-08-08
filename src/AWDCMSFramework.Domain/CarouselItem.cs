using System;
using System.ComponentModel.DataAnnotations;
using AWDCMSFramework.Domain.Interfaces;

namespace AWDCMSFramework.Domain
{
    public class CarouselItem : ILogInfo, IEntity
    {
        public int Id { get; set; }
        public string AltText { get; set; }
        [MaxLength(256)]
        public string LinkURL { get; set; }
        [MaxLength(256)]
        public string ImageURL { get; set; }
        public bool IsLive { get; set; }
        public int SeqNum { get; set; }

        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
