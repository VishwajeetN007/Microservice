namespace StockService.Services.Interfaces
{
    public interface IStockDataService
    {
        bool CheckStockAvailibility(int productId, int quantity);
        bool ReserveStock(int productId, int quantity);
        bool UpdateStock(int productId, int quantity);
    }
}
