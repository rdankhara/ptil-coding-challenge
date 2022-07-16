using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petroineos.PowerServiceImpl
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private string _fileStoreLocation;

        public ConfigurationProvider(NameValueCollection collection)
        {
            _fileStoreLocation = collection.Get("FileStoreLocation");
        }

        public string FileStoreLocation => _fileStoreLocation;
    }
}
