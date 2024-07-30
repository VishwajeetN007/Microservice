using StockService.Database;
using StockService.Database.Entities;
using StockService.Services.Interfaces;

namespace StockService.Services.Implementations
{
    public class StockDataService : IStockDataService
    {
        AppDbContext _db;
        public StockDataService(AppDbContext db)
        {
            _db = db;
        }
        public bool CheckStockAvailibility(int productId, int quantity)
        {
            var stock = _db.Stocks.Where(x => x.ProductId == productId && x.Quantity >= quantity).FirstOrDefault();
            if (stock != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReserveStock(int productId, int quantity)
        {
            var stock = _db.Stocks.Where(x => x.ProductId == productId && x.Quantity >= quantity).FirstOrDefault();
            if (stock != null)
            {
                stock.Quantity = stock.Quantity - quantity;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateStock(int productId, int quantity)
        {
            var stock = _db.Stocks.Where(x => x.ProductId == productId).FirstOrDefault();
            if (stock != null)
            {
                stock.Quantity = stock.Quantity + quantity;
                _db.SaveChanges();
                return true;
            }
            else
            {
                stock = new Stock()
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                _db.Stocks.Add(stock);
                _db.SaveChanges();
                return true;
            }
        }
    }
}
