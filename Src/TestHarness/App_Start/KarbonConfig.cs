using Karbon.Cms.Web;
using TestHarness.App_Start;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(KarbonConfig), "Initialize")]

namespace TestHarness.App_Start
{
    public class KarbonConfig
    {
        public static void Initialize()
        {
            new WebBootManager()
                .Initialize();
        }
    }
}