using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericSearch;
using NUnit.Framework;

namespace GenericSearchUnitTest
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void FindAppleSourCream()
        {
            string[] dataSource = { "dog donkey apple monkey sour", "this planet sour goat", "here is the sour cream" };
            string[] query = { "apple", "sour cream" };

            bool finder = StringSearcher.StringArrayAndSearch(dataSource, query);
            Assert.IsTrue(finder);
        }
    }
}
