using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omdb_search.Extensions;
using omdb_search.Services.Omdb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdb_search.Tests
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public async Task Dictionary_RemoveAll_Correct()
        {
            var testDict = new Dictionary<int, int> { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            testDict.RemoveAll((key, value) => key == 1 || key == 5);
            Assert.IsNotNull(testDict[3]);
            Assert.IsNotNull(testDict[7]);
            Assert.AreEqual(testDict.Count(), 2);
        }
    }
}