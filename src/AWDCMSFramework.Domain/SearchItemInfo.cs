
namespace AWDCMSFramework.Domain
{
    public class SearchItemInfo
    {
        public SearchItemInfo() { }

        public SearchItemInfo(string Title, string Text, string LinkURL)
        {
            this.Title = Title;
            this.Text = Text;
            this.LinkURL = LinkURL;
        }

        public SearchItemInfo(string Title, string Text, string LinkURL, string Module)
        {
            this.Title = Title;
            this.Text = Text;
            this.LinkURL = LinkURL;
            this.Module = Module;
        }

        public string Title { get; set; }
        public string Text { get; set; }
        public string Module { get; set; }
        public string LinkURL { get; set; }
        public int OccurrenceCount { get; set; }
    }
}
