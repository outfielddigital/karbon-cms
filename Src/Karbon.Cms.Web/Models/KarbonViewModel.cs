using Karbon.Cms.Core.Models;

namespace Karbon.Cms.Web.Models
{
    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content.</typeparam>
    /// <typeparam name="THomePageContentType">The type of the home page content.</typeparam>
    public class KarbonViewModel<TCurrentPageContentType, THomePageContentType>
        where TCurrentPageContentType : IContent
        where THomePageContentType : IContent
    {
        /// <summary>
        /// Gets the curret page content.
        /// </summary>
        /// <value>
        /// The current page content.
        /// </value>
        public TCurrentPageContentType CurrentPage { get; private set; }

        /// <summary>
        /// Gets the home page content.
        /// </summary>
        /// <value>
        /// The home page content.
        /// </value>
        public THomePageContentType HomePage { get; private set; }
		 
        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel{TContentType, TSiteType}" /> class.
        /// </summary>
        /// <param name="currentPage">The current page content.</param>
        /// <param name="homePage">The home page content.</param>
        public KarbonViewModel(TCurrentPageContentType currentPage,
            THomePageContentType homePage)
        {
            CurrentPage = currentPage;
            HomePage = homePage;
        }
    }

    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content type.</typeparam>
	public class KarbonViewModel<TCurrentPageContentType> : KarbonViewModel<TCurrentPageContentType, Content>
        where TCurrentPageContentType : IContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel{TContentType}" /> class.
        /// </summary>
        /// <param name="currentPage">The current page content.</param>
        /// <param name="homePage">The home page content.</param>
		public KarbonViewModel(TCurrentPageContentType currentPage, Content homePage)
            : base(currentPage, homePage)
        { }
    }

    /// <summary>
    /// The view model class for Karbon based views.
    /// </summary>
	public class KarbonViewModel : KarbonViewModel<Content, Content>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KarbonViewModel" /> class.
        /// </summary>
        /// <param name="currentPage">The current page content.</param>
        /// <param name="homePage">The home page content.</param>
		public KarbonViewModel(Content currentPage, Content homePage)
            : base(currentPage, homePage)
        { }
    }
}
