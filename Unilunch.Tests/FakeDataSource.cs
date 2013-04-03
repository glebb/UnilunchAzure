using System;
using UnilunchData;

namespace Unilunch.Tests
{
    class FakeDataSource : IDataSource
    {
        public string Data2 { get; set; }
        public string Data { get; set; }
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
