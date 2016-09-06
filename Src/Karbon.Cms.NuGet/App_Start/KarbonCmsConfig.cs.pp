using Karbon.Cms.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof($rootnamespace$.App_Start.KarbonCmsConfig), "Initialize")]
namespace $rootnamespace$.App_Start
{
    public class KarbonCmsConfig
    {
        public static void Initialize()
        {
            new WebBootManager().Initialize();
        }
    }
}