using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using omdb_search.Services.Omdb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace omdb_search.Tests
{
    [TestClass]
    public class OmdbTests
    {
        private static ILogger<OmdbService> logger => new LoggerFactory().CreateLogger<OmdbService>();
        private static OmdbService omdbService => new OmdbService(logger);

        [TestMethod]
        public void ReadResponseData_ReturnsCorrectObject()
        {
            var testJson = @"{""Title"":""Now You See Me"",""Year"":""2013"",""Poster"":""posterLink""}";
            var nameRes = omdbService.ReadResponseData(testJson);
            Assert.AreEqual(nameRes.Title, "Now You See Me");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void ReadResponseData_ThrowsExceptionIfNoTitle()
        {
            var testJson = @"{""Year"":""2013"",""Poster"":""posterLink""}";
            omdbService.ReadResponseData(testJson);
        }

        [TestMethod]
        public async Task SearchByName_ReturnsNoApiKeyError()
        {
            var nameRes = await omdbService.SearchByName("name");
            Assert.IsTrue(nameRes.Contains("No API key provided"));
        }
    }
}