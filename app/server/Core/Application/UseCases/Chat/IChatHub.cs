using Articly.Core.Domain.Entities;

namespace Articly.Core.Application.Chat;

public interface IChatHub
{
    Task ReceivedMessage(ChatMessage message);
}