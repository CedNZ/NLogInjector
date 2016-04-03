using Autofac;
using FluentAssertions;
using NLog;
using NUnit.Framework;

namespace NLogInjector.UnitTests
{
	internal class property_injection_test_class
	{
		public ILogger Logger { get; set; }

		public void LogSomething()
		{
			Logger.Info("Hello world");
		}
	}

	[TestFixture]
    public class nlog_property_injection
    {
		[Test]
	    public void logger_property_should_be_injected_if_used_via_autofac()
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule<NLogModule>();
			containerBuilder.RegisterType<property_injection_test_class>().SingleInstance();

			using (var container = containerBuilder.Build())
			{
				var tempClass = container.Resolve<property_injection_test_class>();
				Assume.That(tempClass, Is.Not.Null);

				tempClass.Logger.Should().NotBeNull();

				tempClass.LogSomething();
			}
		}
    }
}
