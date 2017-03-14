using System.Collections.Specialized;
using Karbon.Cms.Core.Serialization;
using NUnit.Framework;

namespace Karbon.Cms.Tests.Serialization
{
    [TestFixture]
    public class KarbonDataSerializerTests : AbstractDataSerializerTests
    {
        private KarbonDataSerializer _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = new KarbonDataSerializer();
            _serializer.Initialize("karbon", new NameValueCollection());
        }

        [Test]
        public void Can_Deserialize_Stream()
        {
            // Arrange
            var stream = CreateStream(@"Name:Test Name
----
Description: Test Description
----

#################
# Test Comment
#################

Body:

Test Body

----
Multiline: Line1

Line2

----

Ignored
Unclosed:

Test Unclosed");

            // Act
            var result = _serializer.Deserialize(stream);

            // Assert
            Assert.AreEqual(result.Count, 5);
            Assert.IsTrue(result.ContainsKey("Name"));
            Assert.IsTrue(result.ContainsKey("Description"));
            Assert.IsTrue(result.ContainsKey("Body"));
            Assert.IsTrue(result.ContainsKey("Multiline"));
            Assert.IsTrue(result.ContainsKey("Unclosed"));
            Assert.IsTrue(result["Name"] == "Test Name");
            Assert.IsTrue(result["Description"] == "Test Description");
            Assert.IsTrue(result["Multiline"] == "Line1\r\n\r\nLine2");
            Assert.IsTrue(result["Body"] == "Test Body");
            Assert.IsTrue(result["Unclosed"] == "Test Unclosed");
        }
    }
}
