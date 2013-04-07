#region using directives

using System;

#endregion

namespace UnilunchData
{
    public interface IDataSource
    {
        string Load(Uri url);
    }
}