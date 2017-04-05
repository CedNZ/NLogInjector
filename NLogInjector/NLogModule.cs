using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using NLog;
using RuskinDantra.Extensions;

namespace NLogInjector
{
	/// <summary>
	/// Code forked from https://github.com/ziyasal/Autofac.Extras.NLog
	/// </summary>
	public class NLogModule : Autofac.Module
	{
		private static void InjectLoggerProperties(object instance)
		{
			var instanceType = instance.GetType();
			
			var properties = instanceType
				.GetProperties(BindingFlags.Public | BindingFlags.Instance)
				.Where(p => p.PropertyType == typeof(ILogger) && p.CanWrite && p.GetIndexParameters().Length == 0);

			foreach (var propToSet in properties)
			{
				propToSet.SetValue(instance, LogManager.GetLogger(instanceType.FullName), null);
			}
		}

		private static void InjectLoggerFields(object instance)
		{
			var instanceType = instance.GetType();
			
			IEnumerable<FieldInfo> fields = instanceType
				.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
				.Where(p => p.FieldType == typeof(ILogger));

			foreach (var field in fields)
			{
				if (field.HasAttribute<InjectLoggerAttribute>())
					field.SetValue(instance, LogManager.GetLogger(instanceType.FullName));
			}
		}

		private static void OnComponentPreparing(object sender, PreparingEventArgs e)
		{
			var t = e.Component.Activator.LimitType;
			e.Parameters = e.Parameters.Union(
				new[]
				{
					new ResolvedParameter((p, i) => p.ParameterType == typeof (ILogger), (p, i) => LogManager.GetLogger(t.FullName))
				});
		}

		protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
		{
			registration.Preparing += OnComponentPreparing;

			registration.Activated += (sender, e) =>
			{
				InjectLoggerProperties(e.Instance);
				InjectLoggerFields(e.Instance);
			};
		}
	}
}