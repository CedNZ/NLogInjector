using JetBrains.Annotations;
using NLog;

namespace NLogInjector.Sample
{
	[UsedImplicitly]
	internal class LoggerConstructorSample
	{
		private readonly ILogger _logger;
		
		public LoggerConstructorSample(ILogger logger)
		{
			_logger = logger;
		}

		public void LogSomething()
		{
			_logger.Info("Hello world");
			_logger.Error("ERROR");
		}
	}
}