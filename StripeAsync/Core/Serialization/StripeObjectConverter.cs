using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Accounts;
using Brass9.Clients.Stripe.Payment;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Brass9.Clients.Stripe.Core.Serialization
{
	/// <summary>
	/// Stripe objects come in with no declaration of Type; instead they have a property with a string naming the type,
	/// like .object: 'charge' means it's a Charge.
	/// 
	/// Help JsonConvert deserialize these objects.
	/// </summary>
	public class StripeObjectConverter : JsonConverter
	{
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert (Type objectType)
		{
			return typeof (StripeObject).IsAssignableFrom (objectType);
		}

		StripeObject findType (JObject jobj)
		{
			var type = (string) jobj.Property ("object");
			switch (type)
			{
			// TODO
			/*
			case "account":
				return new StripeAccount ();
			case "balance":
				return new StripeBalance ();
			case "balance_transaction":
				return new StripeBalanceTransaction ();
			*/
			case "charge":
				return new Charge ();
			/*
			case "event":
				return new StripeEvent ();
			case "discount":
				return new StripeDiscount ();
			case "dispute":
				return new StripeDispute ();
			case "coupon":
				return new StripeCoupon ();
			*/
			case "customer":
				return new Customer ();
			/*
			case "line_item":
				return new StripeLineItem ();
			case "plan":
				return new StripePlan ();
			case "token":
				return new StripeCreditCardToken ();
			case "subscription":
				return new StripeSubscription ();
			case "invoiceitem":
				return new StripeInvoiceItem ();
			case "invoice":
				return new StripeInvoice ();
			case "transfer":
				return new StripeTransfer ();
			*/
			}

			return new StripeObject ();
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jobj = JObject.Load (reader);

			var target = findType (jobj);

			serializer.Populate (jobj.CreateReader (), target);

			return target;
		}

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			var obj = JObject.FromObject (value);
			obj.WriteTo (writer);
		}
	}
}
