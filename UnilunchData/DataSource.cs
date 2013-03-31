using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public class DataSource : IDataSource
    {
        public string Load(Uri url)
        {
            var contents = new System.Net.WebClient().DownloadString(url);
            return contents;
        }
    }
}
