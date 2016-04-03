using Autofac;
using FluentAssertions;
using NLog;
using NUnit.Framework;

namespace NLogInjector.UnitTests
{
	[TestFixture]
	public class nlog_ctor_injection
	{
		private class ctor_injection_test_class
		{
			private readonly ILogger _logger;

			public ctor_injection_test_class(ILogger logger)
			{
				_logger = logger;
			}

			public bool IsLoggerNull => _logger == null;
		}

		[Test]
		public void logger_property_should_be_injected_if_used_via_autofac()
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule<NLogModule>();
			containerBuilder.RegisterType<ctor_injection_test_class>().SingleInstance();

			using (var container = containerBuilder.Build())
			{
				var tempClass = container.Resolve<ctor_injection_test_class>();
				Assume.That(tempClass, Is.Not.Null);

				tempClass.IsLoggerNull.Should().BeFalse();
			}
		}
	}
}