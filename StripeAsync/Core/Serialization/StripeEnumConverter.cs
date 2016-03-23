using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Core.Serialization
{
	public class StripeEnumConverter<T> : JsonConverter where T : struct, IConvertible
	{
		Dictionary<string, string> values;
		public StripeEnumConverter ()
		{
			values = new Dictionary<string, string> ();
			if (!typeof (T).IsEnum)
				throw new InvalidCastException ("Specified type T must be an enum");
		}

		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof (T);
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null)
				return null;

			var name = reader.Value as string;
			name = name.Replace ("_", "");
			
			return Enum.Parse (typeof (T), name, true);
		}

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			string key = value.ToString ();
			string result;

			if (!values.TryGetValue (key, out result)) {
				result = Regex.Replace (key, @"(?<!^|_|[A-Z])([A-Z])", "_$1").ToLowerInvariant ();
				values [key] = result;
			}
			writer.WriteValue (result);
		}
	}
}
