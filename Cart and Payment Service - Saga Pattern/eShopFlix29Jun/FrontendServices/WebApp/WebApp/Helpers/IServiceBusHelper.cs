using WebApp.Messages;

namespace WebApp.Helpers
{
    public interface IServiceBusHelper
    {
        Task SendPaymentMessage(OrderMessage payload);
    }
}
