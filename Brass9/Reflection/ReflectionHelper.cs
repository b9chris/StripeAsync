using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Brass9.Reflection
{
	public static class ReflectionHelper
	{
		public static PropertyInfo[] GetPublicProperties(Type type)
		{
			var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			return props;
		}
		public static PropertyInfo[] GetPublicProperties<T>()
		{
			return GetPublicProperties(typeof(T));
		}
	}
}
