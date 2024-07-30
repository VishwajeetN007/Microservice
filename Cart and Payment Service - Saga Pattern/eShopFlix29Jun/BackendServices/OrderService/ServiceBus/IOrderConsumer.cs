namespace OrderService.ServiceBus
{
    public interface IOrderConsumer
    {
        Task RegisterReceiveMessageHandler();
    }
}
