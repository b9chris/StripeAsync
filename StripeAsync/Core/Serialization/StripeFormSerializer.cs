using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Reflection;
using Brass9.Reflection;
using Brass9.Clients.Stripe.Core.DateTimeExtensions;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Core.Serialization
{
	/// <summary>
	/// Serializes objects to x-www-form-urlencoded style.
	/// </summary>
	public class StripeFormSerializer
	{
		#region Singleton
		// http://codereview.stackexchange.com/questions/79/implementing-a-singleton-pattern-in-c
		public static StripeFormSerializer O { get { return Nested.instance; } }

		class Nested
		{
			static Nested()
			{
			}

			internal static readonly StripeFormSerializer instance = new StripeFormSerializer();
		}
		#endregion

		protected Dictionary<Type, MethodInfo> converters;

		public StripeFormSerializer()
		{
			var methods = typeof(StripeFormSerializer).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
			string serializeValMethodName = new Func<string, string>(serializeVal).Method.Name;	// Avoid hardcoding
			var serializeMethods = methods.Where(m => m.Name == serializeValMethodName).ToArray();

			converters = new Dictionary<Type, MethodInfo>(serializeMethods.Length);
			foreach(var method in serializeMethods)
			{
				Type converts = method.GetParameters()[0].ParameterType;
				converters.Add(converts, method);
			}
		}



		/// <summary>
		/// Serializes an object into Stripe Form-UrlEncoded format/QueryString serialized format.
		/// 
		/// JSON like this:
		/// 
		/// {
		///   "a": "a",
		///   "b": {
		///     "c": c
		///   }
		/// }
		/// 
		/// becomes:
		/// 
		/// a="a"&b[c]="c"
		/// 
		/// Where child objects use array/dictionary shorthand. Two child levels might look like: b[c[d]]="d"
		/// 
		/// Values are URL-encoded, but names are not.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="o"></param>
		/// <param name="sb"></param>
		/// <returns></returns>
		public string Serialize<T>(T o)
		{
			var sb = new StringBuilder();
			var serializer = new StripeFormSerializerInternal<T>(o, sb, new string[0], false);
			serializer.Serialize();
			return sb.ToString();
		}



		public string SerializeVal(object val)
		{
			// TODO: What to do when there isn't a converter? An obscure null or dictionary exception is going to throw,
			// and really confuse somebody.
			// That said this is designed to only be called by StripeFormSerializerInternal, which will only call here
			// after asking if the value is convertable, so, it should never arise that a Type is passed here that can't
			// be handled.
			var method = converters[val.GetType()];
			return (string)method.Invoke(this, new object[] { val });
		}

		public bool IsLeaf(object val)
		{
			return converters.ContainsKey(val.GetType());
		}



		// Library of converters. Must all be protected, return string, take an overloaded single argument.
		// Automatically picked up at startup via Reflection

		protected string serializeVal(string s)
		{
			return HttpUtility.UrlEncode(s);
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
	}
}
