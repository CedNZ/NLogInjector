using Autofac;
using FluentAssertions;
using NLog;
using NUnit.Framework;

namespace NLogInjector.UnitTests
{
	[TestFixture]
	public class nlog_field_injection
	{
		private class field_injection_test_class
		{
			[InjectLogger]
			private ILogger _logger;

			public bool IsLoggerNull => _logger == null;
		}

		private class field_injection_without_attribute_test_class
		{
			private ILogger _logger;

			public bool IsLoggerNull => _logger == null;
		}

		[Test]
		public void logger_field_should_be_injected_if_used_via_autofac()
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule<NLogModule>();
			containerBuilder.RegisterType<field_injection_test_class>().SingleInstance();

			using (var container = containerBuilder.Build())
			{
				var tempClass = container.Resolve<field_injection_test_class>();
				Assume.That(tempClass, Is.Not.Null);

				tempClass.IsLoggerNull.Should().BeFalse();
			}
		}

		[Test]
		public void logger_field_should_not_be_injected_if_no_attribute_decorated()
		{
			var containerBuilder = new ContainerBuilder();
			containerBuilder.RegisterModule<NLogModule>();
			containerBuilder.RegisterType<field_injection_without_attribute_test_class>().SingleInstance();

			using (var container = containerBuilder.Build())
			{
				var tempClass = container.Resolve<field_injection_without_attribute_test_class>();
				Assume.That(tempClass, Is.Not.Null);

				tempClass.IsLoggerNull.Should().BeTrue();
			}
		}
	}
}