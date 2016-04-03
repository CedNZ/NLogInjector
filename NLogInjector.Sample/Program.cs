using Autofac;

namespace NLogInjector.Sample
{
	class Program
	{
		static void Main(string[] args)
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule<NLogModule>();
			containerBuilder.RegisterType<LoggerPropertySample>().SingleInstance();
			containerBuilder.RegisterType<LoggerConstructorSample>().SingleInstance();
			containerBuilder.RegisterType<LoggerFieldSample>().SingleInstance();

			using (var container = containerBuilder.Build())
			{
				var loggerPropertySample = container.Resolve<LoggerPropertySample>();
				loggerPropertySample.LogSomething();

				var loggerConstructorSample = container.Resolve<LoggerConstructorSample>();
				loggerConstructorSample.LogSomething();

				var loggerFieldSample = container.Resolve<LoggerFieldSample>();
				loggerFieldSample.LogSomething();
			}
		}
	}
}
