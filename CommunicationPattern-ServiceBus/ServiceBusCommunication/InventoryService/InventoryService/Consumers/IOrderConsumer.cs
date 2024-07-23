namespace InventoryService.Consumers
{
    public interface IOrderConsumer
    {
        /// <summary>
        /// It is a listner, which look for the messages inside the service bus queue.
        /// (The message will be there, it will read.)
        /// </summary>
        /// <returns></returns>
        Task RegisterReceiveMessageHandler();
    }
}
