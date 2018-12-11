
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using SignalRWebPack.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace SignalRWebPack.Controllers {
    public class SomeDataController : Controller {
        private readonly IHubContext<ChatHub> _hubContext;
        public SomeDataController(IHubContext<ChatHub> hubContext) {
            _hubContext = hubContext;
        }

        public async void NewMessage() {
            await _hubContext.Clients.All.SendAsync("messageReceived","123","123");
            //await _hubContext.NewMessage("123","123");
        }
    }
}