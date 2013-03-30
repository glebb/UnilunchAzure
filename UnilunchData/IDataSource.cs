using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnilunchData
{
    public interface IDataSource
    {
        string Load(Uri url);
    }
}
