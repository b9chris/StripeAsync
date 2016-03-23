using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Reflection;
using Brass9.Clients.Stripe.Core.DateTimeExtensions;
using Newtonsoft.Json;
using System.Reflection;

namespace Brass9.Clients.Stripe.Core.Serialization
{

	public class StripeDictionaryFormConverter
	{
		#region Singleton
		// http://codereview.stackexchange.com/questions/79/implementing-a-singleton-pattern-in-c
		public static StripeDictionaryFormConverter O { get { return Nested.instance; } }

		class Nested
		{
			static Nested()
			{
			}

			internal static readonly StripeDictionaryFormConverter instance = new StripeDictionaryFormConverter();
		}
		#endregion

		public IEnumerable<KeyValuePair<string, string>> Convert<T>(T o)
		{
			var props = ReflectionHelper.GetPublicProperties<T>();

			var kvs = new KeyValuePair<string, string>[props.Length];

			int i = 0;
			string serializeMethodName = new Func<string, string>(serializeVal).Method.Name;	// Avoid hardcoding the name as a string, get it from reflection
			foreach(var prop in props)
			{
				string name = prop.Name;
				var attrs = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
				if (attrs.Length > 0)
					name = ((JsonPropertyAttribute)attrs.First()).PropertyName;

				//string val = serializeVal(prop.GetValue(o));
				// Instead of above, force concrete method call, below
				// http://stackoverflow.com/a/31059299/176877

				object valObj = prop.GetValue(o);
				var typeArgs = new Type[] { valObj.GetType() };
				var serializeMethod = typeof(StripeDictionaryFormConverter).GetMethod(serializeMethodName,
					BindingFlags.NonPublic | BindingFlags.Instance, null, typeArgs, null);

				string val = (string)serializeMethod.Invoke(this, new object[] { valObj });

				kvs[i++] = new KeyValuePair<string, string>(name, val);
			}

			return kvs;
		}

		protected string serializeVal(object o)
		{
			throw new NotSupportedException("Attempted to serialize a property and couldn't find its real type in the serialization library.");
		}

		protected string serializeVal(string s)
		{
			return s;// HttpUtility.UrlEncode(s);
		}

		protected string serializeVal(DateTime d)
		{
			return d.ToUnixEpoch().ToString();
		}

		protected string serializeVal(int i)
		{
			return i.ToString();
		}

		protected string serializeVal(bool b)
		{
			return b ? "true" : "false";
		}

		protected string serializeVal(Brass9.Clients.Stripe.Accounts.Card card)
		{
			// TODO
			return "";
		}

		protected string serializeVal(Dictionary<string, string> keys)
		{
			// TODO
			return "";
		}
	}
}
