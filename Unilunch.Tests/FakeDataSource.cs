using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnilunchData;

namespace Unilunch.Tests
{
    class FakeDataSource : IDataSource
    {
        public string Data { get; set; }
        public string Load(Uri url)
        {
            return Data;
        }
    }
}
