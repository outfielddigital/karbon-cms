using System.IO;
using System.Text;

namespace Karbon.Cms.Tests.Serialization
{
    public abstract class AbstractDataSerializerTests
    {
        protected Stream CreateStream(string contents)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(contents));
        }
    }
}
