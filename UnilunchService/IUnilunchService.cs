using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using UnilunchData;

namespace UnilunchService
{
    [ServiceContract]
    public interface IUnilunchService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            //RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "restaurants")]
        Message JsonData();
    }
}
