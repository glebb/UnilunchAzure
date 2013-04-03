using System;

namespace UnilunchData
{
    public interface IDataSource
    {
        string Load(Uri url);
    }
}
