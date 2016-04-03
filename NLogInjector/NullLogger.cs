using NLog;

namespace NLogInjector
{
    public class NullLogger
    {
		public static readonly ILogger Instance = LogManager.CreateNullLogger();
	}
}
