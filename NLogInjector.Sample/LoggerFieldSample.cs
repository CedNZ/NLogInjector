using JetBrains.Annotations;
using NLog;

namespace NLogInjector.Sample
{
	[UsedImplicitly]
	internal class LoggerFieldSample
	{
		private readonly ILogger _logger = NullLogger.Instance;

		public LoggerFieldSample()
		{
			// NB: cannot use logger in ctor as it is only initialized after object is constructed
		}

		public void LogSomething()
		{
			_logger.Info("Hello world");
			_logger.Error("ERROR");
		}
	}
}