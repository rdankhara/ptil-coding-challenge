using NUnit.Framework;
using System.Collections.Specialized;

namespace Petroineos.PowerServiceImpl.Tests
{
    [TestFixture]
    public class ConfigurationProviderTests
    {
        private IConfigurationProvider configurationProvider;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VerifyThatConfigurationProviderInitialisedWithAppConfig()
        {
            var collection = new NameValueCollection();
            collection.Set("FileStoreLocation", "ABCD");
            configurationProvider = new ConfigurationProvider(collection);

            Assert.That(configurationProvider.FileStoreLocation, Is.EqualTo("ABCD"));
        }
    }
}
