using OrderService.Database.Entities;

namespace OrderService.Services.Interfaces
{
    public interface IOrderDataService
    {
        List<Order> GetAllOrder();
        void SaveOrder(Order order, long cartId);
        Order GetOrder(Guid orderId);
        Task<bool> AcceptedOrder(Guid OrderId, DateTime AcceptedDateTime);
        Task<bool> DeleteOrder(Guid orderId);
    }
}
