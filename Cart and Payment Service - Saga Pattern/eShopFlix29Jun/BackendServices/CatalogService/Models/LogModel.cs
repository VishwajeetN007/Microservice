namespace CatalogService.Models
{
    public class LogModel : Exception
    {
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string StackTraceString { get; set; }
    }
}
