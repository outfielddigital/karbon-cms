using Karbon.Cms.Core.Models;
using Karbon.Cms.Web.Parsers.KarbonText;
using NUnit.Framework;

namespace Karbon.Cms.Tests.Parsing
{
    [TestFixture]
    public class TagParserTests
    {
        [Test]
        public void Can_Parse_Tags()
        {
            // Arrange
            var content = new Content();
            var parser = KarbonTextParser.Instance;
            var input1 = "Lorem ipsum [ LINK : http://www.google.com ] dolar.";
            var input2 = "Lorem ipsum [ link:http://www.google.com | title:Something something | text:Click here ] dolar.";

            // Act
            var output1 = parser.ParseTags(content, input1);
            var output2 = parser.ParseTags(content, input2);

            // Assert
            Assert.AreEqual(output1, "Lorem ipsum <a href=\"http://www.google.com\">http://www.google.com</a> dolar.");
            Assert.AreEqual(output2, "Lorem ipsum <a href=\"http://www.google.com\" title=\"Something something\">Click here</a> dolar.");
        }
    }
}
