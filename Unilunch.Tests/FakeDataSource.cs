#region using directives

using System;
using UnilunchData;

#endregion

namespace Unilunch.Tests
{
    internal class FakeDataSource : IDataSource
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