using Microsoft.AspNetCore.SignalR;

namespace BlazorAppForSignalR.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage1(string me, string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage1", me, user, message);
    }

    public async Task SendMessage(string me, string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", me, user, message);
    }
}