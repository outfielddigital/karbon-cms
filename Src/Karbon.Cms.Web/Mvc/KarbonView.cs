using System.Web.Mvc;
using Karbon.Cms.Core.Models;
using Karbon.Cms.Web.Models;

namespace Karbon.Cms.Web.Mvc
{
    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content.</typeparam>
    /// <typeparam name="THomePageContentType">The type of the home page content.</typeparam>
    public abstract class KarbonView<TCurrentPageContentType, THomePageContentType> : WebViewPage<KarbonViewModel<TCurrentPageContentType, THomePageContentType>>
        where TCurrentPageContentType : IContent
		where THomePageContentType : IContent
    {
        /// <summary>
        /// Sets the view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if (viewData.Model is TCurrentPageContentType)
            {
                viewData.Model = new KarbonViewModel<TCurrentPageContentType, THomePageContentType>(
                    (TCurrentPageContentType)viewData.Model,
					(THomePageContentType)KarbonWebContext.Current.HomePage);

				base.SetViewData(viewData); 
            }
			else if (viewData.Model.GetType().GetGenericTypeDefinition() == typeof (KarbonViewModel<,>))
			{
				var tmp = viewData.Model as dynamic;
				base.SetViewData(new ViewDataDictionary(viewData)
				{
					Model = new KarbonViewModel<TCurrentPageContentType, THomePageContentType>(
						(TCurrentPageContentType)tmp.CurrentPage,
						(THomePageContentType)tmp.HomePage)
				}); 
			}
			else
			{
				base.SetViewData(viewData); 
			}
        }
    }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
    /// <typeparam name="TCurrentPageContentType">The type of the current page content.</typeparam>
	public abstract class KarbonView<TCurrentPageContentType> : KarbonView<TCurrentPageContentType, Content>
		where TCurrentPageContentType : IContent
    { }

    /// <summary>
    /// The base class for Karbon based views.
    /// </summary>
	public abstract class KarbonView : KarbonView<Content, Content>
    { }
}
