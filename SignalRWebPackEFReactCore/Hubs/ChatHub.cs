using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRWebPack.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.messageReceived(username, message);
        }
    }
}