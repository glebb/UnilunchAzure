#region using directives

using System;
using System.Net;
using System.Text;

#endregion

namespace UnilunchData
{
    public class DataSource : IDataSource
    {
        public string Load(Uri url)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                var contents = webClient.DownloadString(url);
                return contents;
            }
        }
    }
}