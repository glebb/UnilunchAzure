using System;
using UnilunchData;

namespace Unilunch.Tests
{
    class FakeDataSource : IDataSource
    {
        public string Data2 { private get; set; }
        public string Data { private get; set; }
        public string Load(Uri url)
        {
            if (!url.ToString().EndsWith("piato"))
            {
                return Data2;
            }
            return Data;
        }
    }
}
