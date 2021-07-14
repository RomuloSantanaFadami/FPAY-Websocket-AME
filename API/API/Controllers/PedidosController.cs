using API.Hub.Hubs;
using API.Hubs.Interfaces;
using API.Models.Transacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/{action}")]
    public class PedidosController : ControllerBase
    {

        private readonly ILogger<PedidosController> _logger;
        private readonly IHubContext<PedidosHub, IPedidosHub> _hubContext;

        public PedidosController(ILogger<PedidosController> logger, IHubContext<PedidosHub, IPedidosHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        [HttpPost]
        public CriacaoPedidoResponseModel CreateOrder([FromBody] CriacaoPedidoRequestModel pedido)
        {
            _logger.LogInformation($"Criação de pedido: ${ JsonConvert.SerializeObject(pedido) }");
            pedido.attributes.transactionChangedCallbackUrl = this.GetBaseUrlCallback(Request);
            //pedido.attributes.transactionChangedCallbackUrl = null;

            _logger.LogInformation($"pedido.attributes.transactionChangedCallbackUrl -> { pedido.attributes.transactionChangedCallbackUrl })");
            return CreateOrderOnAME(pedido);
        }


        private CriacaoPedidoResponseModel CreateOrderOnAME(CriacaoPedidoRequestModel pedido)
        {
            _logger.LogInformation($"Criação de pedido na AME -> ${ JsonConvert.SerializeObject(pedido) }");
            var client = new RestClient("http://api-amedigital.sensedia.com/hml/transacoes/v1/ordens");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("client_id", "8ec8db06-2cc9-3c8c-a1e8-3a5e4a17275c");
            request.AddHeader("access_token", "9258dc04-1163-3301-a5ee-004a3b9c3b57");
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(pedido);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            _logger.LogInformation($"CAPTURE REQUEST -> {body}");

            IRestResponse response = client.Execute(request);
            _logger.LogInformation($"Criação de pedido na AME StatusCode -> {response.StatusCode}");
            _logger.LogInformation($"Criação de pedido na AME Content    -> {response.Content}");

            return JsonConvert.DeserializeObject<CriacaoPedidoResponseModel>(response.Content);
        }

        #region Novo Action de callback para teste
        [HttpPost]
        public ActionResult CreateOrderCallback([FromBody] object pedidoCallback)
        {
            _logger.LogInformation($"POST | CreateOrderCallback -> object value: {pedidoCallback}");
            return Ok(pedidoCallback);
        }
        #endregion

        #region  Action Original de Callback

        //[HttpPost]
        //public CriacaoPedidoCallbackModel CreateOrderCallback([FromBody] CriacaoPedidoCallbackModel pedidoCallback)
        //{
        //    _logger.LogInformation($"POST | CreateOrderCallback ->value: { JsonConvert.SerializeObject(pedidoCallback)}");
        //    // sinalizar SLM do retorno
        //    var conn = PedidosHub.Connections.GetConnectionIdSlm(pedidoCallback.attributes.customPayload.isFrom);

        //    if (!string.IsNullOrEmpty(conn))
        //        _hubContext.Clients.Client(conn).ReceiveCreateOrderCallback(pedidoCallback);
        //    else
        //        _logger.LogInformation($"POST | CreateOrderCallback -> cliente não encontrado");

        //    return pedidoCallback;
        //} 
        #endregion


        //private bool CaptureTransaction(UrlCallbackResponseModel transaction)
        //{
        //    var client = new RestClient("http://api-amedigital.sensedia.com/hml/transacoes/v1/pagamentos");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("client_id", "8ec8db06-2cc9-3c8c-a1e8-3a5e4a17275c");
        //    request.AddHeader("access_token", "9258dc04-1163-3301-a5ee-004a3b9c3b57");
        //    request.AddHeader("Content-Type", "application/json");
        //    var body = JsonConvert.SerializeObject(new CapturePagamentoModel { idPagamento = transaction.id });
        //    request.AddParameter("application/json", body, ParameterType.RequestBody);

        //    _logger.LogInformation($"CAPTURE REQUEST -> {body}");

        //    IRestResponse response = client.Execute<CapturePagamentoResponseModel>(request);
        //    _logger.LogInformation($"CAPTURE RESPONSE -> {response.StatusCode}");
        //    _logger.LogInformation($"CAPTURE RESPONSE -> {response.Content}");

        //    return true;
        //}


        private string GetBaseUrlCallback(HttpRequest request)
        {
            if (request.Host.HasValue)
                return $"{request.Scheme}://" +
                    $"{request.Host.Value}" +
                    $"{(request.PathBase.HasValue ? request.PathBase.Value : "")}" +
                    $"{(request.Path.HasValue ? request.Path.Value : "")}" +
                    // $"{(request.RouteValues.Count > 0 ? "/" + string.Join("/", request.RouteValues.Values.Where((x, y) => y < request.RouteValues.Count - 1).Select(x => x)): "")}"

                    "Callback"
                    ;

            return null;
        }
    }
}
