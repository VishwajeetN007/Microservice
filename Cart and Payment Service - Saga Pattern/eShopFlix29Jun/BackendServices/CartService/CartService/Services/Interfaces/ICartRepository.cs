using CartService.Database.Entities;
using CartService.Models;

namespace CartService.Services.Interfaces
{
    public interface ICartRepository
    {
        Task<CartModel> GetUserCart(long UserId);
        int GetCartItemCount(int CartId);
        IEnumerable<CartItem> GetCartItems(long CartId);
        Task<CartModel> GetCart(int CartId);
        Cart AddItem(long UserId, long CartId, int ItemId, decimal UnitPrice, int Quantity);
        int DeleteItem(int CartId, int ItemId);
        bool MakeInActive(int CartId);
        int UpdateQuantity(int CartId, int ItemId, int Quantity);
    }
}
