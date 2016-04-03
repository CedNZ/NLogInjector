using JetBrains.Annotations;
using NLog;

namespace NLogInjector.Sample
{
	internal class LoggerPropertySample
	{
		[UsedImplicitly]
		public ILogger Logger { get; set; }

		public void LogSomething()
		{
			Logger.Info("Hello world");
			Logger.Error("ERROR");
		}
	}
}