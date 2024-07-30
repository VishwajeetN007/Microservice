using CartService.Database;
using CartService.Database.Entities;
using CartService.HttpClients;
using CartService.Models;
using CartService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace CartService.Services.Implementations
{
    public class CartRepository : ICartRepository
    {
        AppDbContext _db;
        IConfiguration _configuration;
        CatalogService _catalogService;
        public CartRepository(AppDbContext db, IConfiguration configuration, CatalogService catalogService)
        {
            _db = db;
            _configuration = configuration;
            _catalogService = catalogService;
        }

        public Cart AddItem(long UserId, long CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            Cart cart = new Cart();
            if (CartId > 0)
            {
                cart = _db.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == CartId && c.IsActive == true);
            }
            else
                cart = _db.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserId == UserId && c.IsActive == true);

            if (cart != null)
            {
                CartItem cartItem = cart.CartItems.FirstOrDefault(ci => ci.ItemId == ItemId);
                if (cartItem != null)
                {
                    cartItem.Quantity += Quantity;
                    _db.SaveChanges();
                }
                else
                {
                    cartItem = new CartItem
                    {
                        ItemId = ItemId,
                        UnitPrice = UnitPrice,
                        Quantity = Quantity
                    };
                    cart.CartItems.Add(cartItem);
                    _db.SaveChanges();
                }
            }
            else
            {
                cart = new Cart
                {
                    UserId = UserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                CartItem cartItem = new CartItem
                {
                    ItemId = ItemId,
                    UnitPrice = UnitPrice,
                    Quantity = Quantity
                };
                cart.CartItems.Add(cartItem);
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            return cart;
        }
       
        public int DeleteItem(int CartId, int Id)
        {
            CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.CartId == CartId && ci.Id == Id);
            if (cartItem != null)
            {
                _db.CartItems.Remove(cartItem);
                _db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public async Task<CartModel> GetCart(int CartId)
        {
            var cart = _db.Carts.Include(c => c.CartItems).Where(c => c.Id == CartId && c.IsActive == true).FirstOrDefault();

            //get Ids
            var productIds = cart.CartItems.Select(ci => ci.ItemId).ToList();
            var tasks = productIds.Select(id => _catalogService.GetProductAsync(id)).ToList();
            var products = await Task.WhenAll(tasks);

            if (cart != null)
            {
                CartModel cartModel = new CartModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CreatedDate = cart.CreatedDate,
                    IsActive = cart.IsActive,
                    CartItems = (from ci in cart.CartItems
                                join p in products on ci.ItemId equals p.ProductId
                                select new CartItemModel
                                {
                                    Id = ci.Id,
                                    ItemId = ci.ItemId,
                                    UnitPrice = ci.UnitPrice,
                                    Quantity = ci.Quantity,
                                    Name = p.Name,
                                    ImageUrl = p.ImageUrl
                                }).ToList()                    
                };
                if (cartModel.CartItems.Count > 0)
                {
                    foreach (var item in cartModel.CartItems)
                    {
                        cartModel.Total += item.UnitPrice * item.Quantity;
                    }
                    cartModel.Tax = cartModel.Total * Math.Round(Convert.ToDecimal(_configuration["Tax"]) / 100, 2);
                    cartModel.GrandTotal = cartModel.Total + cartModel.Tax;
                }
                return cartModel;
            }
            return null;
        }

        public async Task<CartModel> GetUserCart(long UserId)
        {
            var cart = _db.Carts.Include(c => c.CartItems).Where(c => c.UserId == UserId && c.IsActive == true).FirstOrDefault();

            if (cart != null)
            {
                //get Ids
                var productIds = cart.CartItems.Select(ci => ci.ItemId).ToList();
                var tasks = productIds.Select(id => _catalogService.GetProductAsync(id)).ToList();
                var products = await Task.WhenAll(tasks);

                CartModel cartModel = new CartModel
                {
                    Id = cart.Id,
                    UserId = cart.UserId,
                    CreatedDate = cart.CreatedDate,
                    IsActive = cart.IsActive,
                    CartItems = (from ci in cart.CartItems
                                 join p in products on ci.ItemId equals p.ProductId
                                 select new CartItemModel
                                 {
                                     Id = ci.Id,
                                     ItemId = ci.ItemId,
                                     UnitPrice = ci.UnitPrice,
                                     Quantity = ci.Quantity,
                                     Name = p.Name,
                                     ImageUrl = p.ImageUrl,
                                     CartId = ci.CartId
                                 }).ToList()
                };
                if (cartModel.CartItems.Count > 0)
                {
                    foreach (var item in cartModel.CartItems)
                    {
                        cartModel.Total += item.UnitPrice * item.Quantity;
                    }
                    cartModel.Tax = cartModel.Total * Math.Round(Convert.ToDecimal(_configuration["Tax"]) / 100, 2);
                    cartModel.GrandTotal = cartModel.Total + cartModel.Tax;
                }
                return cartModel;
            }
            return null;
        }

        public int GetCartItemCount(int CartId)
        {
            return _db.CartItems.Where(ci => ci.CartId == CartId).Sum(ci => ci.Quantity);
        }

        public int UpdateQuantity(int CartId, int Id, int Quantity)
        {
            CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.CartId == CartId && ci.Id == Id);
            if (cartItem != null)
            {
                cartItem.Quantity += Quantity;
                _db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public bool MakeInActive(int CartId)
        {
            Cart cart = _db.Carts.FirstOrDefault(c => c.Id == CartId);
            if (cart != null)
            {
                cart.IsActive = false;
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<CartItem> GetCartItems(long CartId)
        {
            return _db.CartItems.Where(ci => ci.CartId == CartId).ToList();
        }
    }
}
