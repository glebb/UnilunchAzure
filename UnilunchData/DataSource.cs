using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace UnilunchData
{
    public class DataSource : IDataSource
    {
        public string Load(Uri url)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                var contents = webClient.DownloadString(url);
                return contents;
            }
        }
    }
}
