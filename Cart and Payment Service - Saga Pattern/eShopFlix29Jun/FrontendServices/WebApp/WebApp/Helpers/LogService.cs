using Serilog;

namespace WebApp.Helpers
{
    public static class LogService
    {
        public static void LogError(Exception ex)
        {
            Log.Error(ex,ex.Message);
            
        }
    }
}
