using APIChat.Model;

namespace APIChat.Hubs
{
    public interface IHubProvider
    {
        Task ReceivedMessage(Message message);
    }
}
