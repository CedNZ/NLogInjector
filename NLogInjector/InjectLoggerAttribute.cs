using System;
using JetBrains.Annotations;

namespace NLogInjector
{
	[MeansImplicitUse, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class InjectLoggerAttribute : Attribute
	{
		
	}
}