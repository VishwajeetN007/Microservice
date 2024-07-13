namespace InventoryService.Consumers
{
    public interface IOrderConsumer
    {
        Task RegisterReceiveMessageHandler();
    }
}
