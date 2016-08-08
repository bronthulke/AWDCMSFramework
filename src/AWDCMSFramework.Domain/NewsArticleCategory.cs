using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWDCMSFramework.Domain
{
    public class NewsArticleCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public virtual ICollection<NewsArticle> NewsArticles { get; set; }

    }
}
