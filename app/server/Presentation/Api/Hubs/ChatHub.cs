using Articly.Core.Application.Chat;
using Articly.Core.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Articly.Presentation.Api.Hubs;

public class ChatHub : Hub<IChatHub>
{
    public async Task SendMessage(ChatMessage message)
    {
        await Clients.All.ReceivedMessage(message);
    }
}