﻿#region using directives

using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

#endregion

namespace UnilunchService
{
    [ServiceContract]
    public interface IUnilunchService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "restaurants?date={date}")]
        Stream FetchData(string date);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "updatedb")]
        Stream UpdateDatabase();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "restaurantsAll")]
        Stream AllData();
    }
}