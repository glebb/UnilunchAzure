using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using UnilunchData;

namespace UnilunchService
{
    [ServiceContract]
    public interface IUnilunchServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            //ResponseFormat = WebMessageFormat.Json,
            //RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "unilunch/restaurants")]
        Message JsonData();
    }
}
