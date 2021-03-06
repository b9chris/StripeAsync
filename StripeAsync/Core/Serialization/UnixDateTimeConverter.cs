﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Brass9.Clients.Stripe.Core.DateTimeExtensions;

namespace Brass9.Clients.Stripe.Core.Serialization
{
	/// <summary>
	/// Handle Stripe's Unix-formatted Dates
	/// </summary>
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		static bool IsNullable (Type type)
		{
			if (!type.IsValueType)
				return true; // ref-type
			if (Nullable.GetUnderlyingType (type) != null)
				return true; // Nullable<T>
			return false; // value-type
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool nullable = IsNullable (objectType);
			Type t = (nullable) ? Nullable.GetUnderlyingType (objectType) : objectType;
			if (reader.TokenType == JsonToken.Null) {
				if (!nullable)
					throw new Exception (String.Format ("Cannot convert null value to {0}.", objectType));
				return null;
			}

			if (reader.TokenType != JsonToken.Integer)
				throw new Exception (String.Format ("Unexpected token parsing date. Expected Integer, got {0}.", reader.TokenType));

			return ((long) reader.Value).FromUnixEpoch ();
		}

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!(value is DateTime))
				throw new Exception ("Invalid value");

			DateTime dt = (DateTime) value;
			writer.WriteValue (dt.ToUnixEpoch ());
		}
	}}
