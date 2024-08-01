namespace LogService.Models
{
    public class ExceptionModel: Exception
    {
        public string ClassName { get; set; }
        public new string Message { get; set; }
        public string StackTraceString { get; set; }
    }
}
