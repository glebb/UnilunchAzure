using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
namespace UnilunchService
{
    [ServiceContract]
    public interface IUnilunchServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "unilunch/restaurants")]
        Restaurant JsonData();
    }
}
