#region using directives

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
            UriTemplate = "restaurants?date={date}&name={name}&id={id}")]
        Stream FetchData(string date, string name, string id);

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