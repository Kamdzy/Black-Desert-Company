
namespace BDCompany.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using HtmlAgilityPack;

    using TinyLittleMvvm;

    /// <summary>
    ///     The official news view model.
    /// </summary>
    internal class OfficialNewsViewModel : PropertyChangedBase, IShell
    {
        
        /// <summary>
        ///     The news links.
        /// </summary>
        private readonly List<string> newsLinks = new List<string>();

        /// <summary>
        ///     The news titles.
        /// </summary>
        private readonly List<string> newsTitles = new List<string>();

        /// <summary>
        /// The pages.
        /// </summary>
        private readonly List<string> pages = new List<string>
                                                 {
                                                     "list?page=1",
                                                     "list?page=2",
                                                     "list?page=3"
                                                 };

        /// <summary>
        ///     Initializes a new instance of the <see cref="OfficialNewsViewModel" /> class.
        /// </summary>
        public OfficialNewsViewModel()
        {
            const string Baseurl = "https://www.blackdesertonline.com/news/";

            foreach (var page in this.pages)
            {
                var mixedurl = Baseurl + page;
                var web = new HtmlWeb();
                var htmlDoc = web.Load(mixedurl);

                foreach (var title in htmlDoc.DocumentNode.SelectNodes(
                    "//span[@class='cont_news']//strong[@class='tit_news']"))
                {
                    this.newsTitles.Add(title.InnerText);
                }

                foreach (var link in htmlDoc.DocumentNode.SelectNodes("//ul[@class='list_news_type2']//a[@href]"))
                {
                    var newsLink = link.GetAttributeValue("href", string.Empty);
                    this.newsLinks.Add(Baseurl + newsLink);
                }

                for (var i = 0; i < this.newsLinks.Count; i++)
                {
                    this.Items.Add(new NewsBlock { Title = this.newsTitles[i], Link = this.newsLinks[i] });
                }
            }
        }
       
        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public List<NewsBlock> Items { get; set; } = new List<NewsBlock>();
    }

    /// <summary>
    /// The news block.
    /// </summary>
    public class NewsBlock
    {
        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }
    }
}