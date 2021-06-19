using API.Models.Transacoes;
using System.Threading.Tasks;

namespace API.Hubs.Interfaces
{
    public interface IPedidosHub
    {
        Task ReceiveCreateOrderCallback(CriacaoPedidoCallbackModel callback);
    }
}
