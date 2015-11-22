using GoldMine.DataModel.Request;
using GoldMine.DataModel.Response;
using GoldMine.ServerBase.Util;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.ModelBinding;

namespace GoldMine.MainServer
{
    public class GoldMineWebService : NancyModule, IApplicationStartup
    {
        public GoldMineWebService()
        {
            Get["/helloworld"] = _ =>
            {
                return Response.AsJson(logic.HelloWorld());
            };

            Post["/register"] = _ =>
            {
                var request = this.Bind<RequestRegister>();
                return Response.AsJson(logic.Register(request));
            };
        }

        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError += OnException;
        }

        private dynamic OnException(NancyContext ctx, System.Exception ex)
        {
            logic.OnException(ex);
            int code = ex.GetExceptionCode();
            var response = Response.AsJson(new ResponseError(code));
            response.StatusCode = (HttpStatusCode)code;
            return response;
        }

        private ServiceLogic logic = Singleton<ServiceLogic>.Instance;
    }
}