using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using Brass9.Reflection;
using Brass9.Clients.Stripe.Core.DateTimeExtensions;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Core.Serialization
{
	public class StripeFormSerializerInternal
	{
		public static void CreateAndSerialize<T>(T o, StringBuilder sb, string[] parentChain, bool fencePost)
		{
			var serializer = new StripeFormSerializerInternal<T>(o, sb, parentChain, fencePost);
			serializer.Serialize();
		}
	}

	public class StripeFormSerializerInternal<T>
	{
		protected T o;
		protected StringBuilder sb;
		protected string[] parentChain;
		protected bool fencePost;	// Whether we're at the beginning or need to emit a & before writing name/val pair

		public StripeFormSerializerInternal(T o, StringBuilder sb, string[] parentChain, bool fencePost)
		{
			this.o = o;
			this.sb = sb;
			this.parentChain = parentChain;
			this.fencePost = fencePost;
		}

		public void Serialize()
		{
			var props = ReflectionHelper.GetPublicProperties<T>();

			foreach(var prop in props)
			{
				object value = prop.GetValue(o);
				if (value == null)
					continue;

				serializeProp(prop, value);
			}
		}

		protected void serializeProp(PropertyInfo prop, object val)
		{
			// Skip props with JsonIgnore applied.
			if (prop.GetCustomAttribute(typeof(JsonIgnoreAttribute)) != null)
				return;

			if (StripeFormSerializer.O.IsLeaf(val))
				serializeLeaf(prop, val);
			else
				serializeBranch_R(prop, val);
		}

		protected void serializeLeaf(PropertyInfo prop, object val)
		{
			if (fencePost)
				sb.Append('&');
			else
				fencePost = true;

			serializeName(prop, sb, parentChain);

			sb.Append('=');

			string serialized = StripeFormSerializer.O.SerializeVal(val);
			//string serialized = serializeVal(prop.GetValue(val));

			sb.Append(serialized);
		}

		protected void serializeBranch_R(PropertyInfo prop, object val)
		{
			string name = propName(prop);
			string[] childParentChain = parentChain.Concat(new string[] { name }).ToArray();
			var type = val.GetType();
			var action = new Action<object, StringBuilder, string[], bool>(StripeFormSerializerInternal.CreateAndSerialize<object>);
			var method = action.Method.GetGenericMethodDefinition().MakeGenericMethod(type);
			method.Invoke(null, new object[] { val, sb, childParentChain, fencePost });
		}


		protected string propName(PropertyInfo prop)
		{ 
			string name = prop.Name;
			var attrs = prop.GetCustomAttributes(typeof(JsonPropertyAttribute), false);
			if (attrs.Length > 0)
				name = ((JsonPropertyAttribute)attrs.First()).PropertyName;
			return name;
		}

		/// <summary>
		/// Emits the chained name into the StringBuilder, like card[address] if "address" in the card child object.
		/// </summary>
		/// <param name="prop">Prop to emit the name for</param>
		/// <param name="sb">StringBuilder to emit to</param>
		/// <param name="parentChain">Existing chain of parents, if any</param>
		/// <returns>The unchained name, like "address" in the above example, for addition to parentChain</returns>
		protected string serializeName(PropertyInfo prop, StringBuilder sb, string[] parentChain)
		{
			string name = propName(prop);

			if (parentChain != null)
			{
				foreach(var parent in parentChain)
				{
					sb.Append(parent)
						.Append('[');
				}
			}

			sb.Append(name);

			if (parentChain != null)
			{
				for (int i = parentChain.Length; i > 0; i--)
				{
					sb.Append(']');
				}
			}

			return name;
		}
	}
}
