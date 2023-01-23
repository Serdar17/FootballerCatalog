using Microsoft.AspNetCore.SignalR;

namespace Web.Context;

public class PlayerHub : Hub
{
    public async Task Send(string message)
    {
        await Clients.All.SendAsync("Send", message);
    }
}