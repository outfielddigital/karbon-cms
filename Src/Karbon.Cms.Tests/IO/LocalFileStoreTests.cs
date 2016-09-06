using System.Collections.Specialized;
using Karbon.Cms.Core.IO;
using NUnit.Framework;

namespace Karbon.Cms.Tests.IO
{
    [TestFixture]
    public class LocalFileStoreTests
    {
        [Test]
        public void Can_Get_Directory_Name()
        {
            // Arrange
            var relativePath = "01-test/02-test2/03-test3";
            var store = new LocalFileStore();
            store.Initialize("content", new NameValueCollection
            {
                {"pathSeperator", "/"},
                {"rootPhysicalPath", ""}
            });

            // Act
            var dirName = store.GetName(relativePath);

            // Assert
            Assert.AreEqual("03-test3", dirName);
        }
    }
}
