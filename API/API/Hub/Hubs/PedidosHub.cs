using API.Hubs.Interfaces;
using API.Models;
using API.Models.Transacoes;
using API.Repositories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Hub.Hubs
{
    public class PedidosHub : Hub<IPedidosHub>, IPedidosHub
    {
        public readonly static ConnectionsRepository Connections = new ConnectionsRepository();

        public override Task OnConnectedAsync()
        {
            var slm = JsonConvert.DeserializeObject<SLMModel>(Context.GetHttpContext().Request.Query["slm"]);
            Connections.Add(slm.Id, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public async Task ReceiveCreateOrderCallback(CriacaoPedidoCallbackModel callback)
        {
            var connection = Connections.GetConnectionIdSlm(callback.attributes.customPayload.isFrom);
            // return await Clients.Client(connection).SendAsync("CreateOrderCallback", callback);
            await Clients.Client(connection).ReceiveCreateOrderCallback(callback);
        }
    }
}
