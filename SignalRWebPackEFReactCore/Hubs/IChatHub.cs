using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SignalRWebPack.Hubs {
    public interface IChatHub {
        Task messageReceived(string username, string message);
    }
}
