using OrderService.Database;
using OrderService.Database.Entities;
using OrderService.HttpClients;
using OrderService.Services.Interfaces;

namespace OrderService.Services.Implementations
{
    public class OrderDataService : IOrderDataService
    {
        AppDbContext context;
        CartService _cartService;
        public OrderDataService(AppDbContext _context, CartService cartService)
        {
            context = _context;
            _cartService = cartService;
        }
        public List<Order> GetAllOrder()
        {
            return context.Orders.ToList();
        }
        public void SaveOrder(Order order, long cartId)
        {
            try
            {                
                var items = _cartService.GetCartItemsAsync(cartId).Result;
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderId = order.OrderId;
                        orderItem.ItemId = item.ItemId;
                        orderItem.Quantity = item.Quantity;
                        orderItem.UnitPrice = item.UnitPrice;
                        orderItem.Total = item.Quantity * item.UnitPrice;
                        order.OrderItems.Add(orderItem);
                    }
                }
                context.Orders.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            try
            {
                Order order = context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();

                if (order != null)
                {
                    context.Remove(order);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AcceptedOrder(Guid OrderId, DateTime AcceptedDateTime)
        {
            try
            {
                var order = context.Orders.Where(x => x.OrderId == OrderId).FirstOrDefault();
                if (order != null)
                {
                    order.AcceptDate = AcceptedDateTime;
                    context.Orders.Update(order);

                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }
        public Order GetOrder(Guid orderId)
        {
            return context.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();
        }
    }
}
