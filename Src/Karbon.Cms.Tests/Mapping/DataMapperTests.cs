using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Karbon.Cms.Core.Mapping;
using Karbon.Cms.Core.Models;
using NUnit.Framework;

namespace Karbon.Cms.Tests.Mapping
{
    [TestFixture]
    public class DataMapperTests
    {
        [Test]
        public void Can_Map_Data()
        {
            // Arrange
            var data = new Dictionary<string, string>
            {
                {"Title", "Test Title"},
                {"Description", "Test Description"},
                {"Date", "2013-01-01"},
                {"Int", "21"},
                {"Bool", "True"},
                {"IntList", "1,2,3"},
                {"Misc", "Test Misc"}
            };
            var content = new TestContent();

            // Act
            content = new DataMapper().Map(content, data);

            // Assert
            Assert.AreEqual("Test Title", content.Title);
            Assert.AreEqual("Test Description", content.Description);
            Assert.AreEqual(new DateTime(2013, 1, 1), content.Date);
            Assert.AreEqual(21, content.Int);
            Assert.AreEqual(1, content.Data.Count);
            Assert.IsTrue(content.Bool);
            Assert.AreEqual(3, content.IntList.Count);
            Assert.IsTrue(content.Data.ContainsKey("Misc"));
            Assert.AreEqual("Test Misc", content.Data["Misc"]);
        }
    }

    #region Helper Classes

    public class TestContent : Content
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Int { get; set; }
        public bool Bool { get; set; }

        [TypeConverter(typeof(IntListTypeConverter))]
        public IList<int> IntList { get; set; }
    }

    public class IntListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, 
            System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
                return ((string)value).Split(',').Select(int.Parse).ToList();

            return base.ConvertFrom(context, culture, value);
        }
    }

    #endregion
}
