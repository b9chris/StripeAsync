using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	// https://stripe.com/docs/api#create_charge
	public class BaseChargeInput
	{
		[JsonProperty (PropertyName = "amount")]
		public int Amount { get; set; }

		[JsonProperty (PropertyName = "currency")]
		public string Currency { get; set; }

		/// <summary>
		/// A Stripe-only description. Appears in the internal Stripe interface when perusing charges on Stripe.com,
		/// and in the Stripe-sent receipt email, if you use Stripe to send one (don't do that).
		/// </summary>
		[JsonProperty (PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty (PropertyName = "metadata")]
		public Dictionary<string, string> Metadata { get; set; }

		[JsonProperty (PropertyName = "capture")]
		public bool Capture { get; set; }

		/// <summary>
		/// Note to appear on customer's statement.
		/// Up to 22 characters.
		/// ASCII only.
		/// May not include <>'".
		/// May render poorly with some banks.
		/// </summary>
		[JsonProperty (PropertyName = "statement_descriptor")]
		public string CardStatement { get; set; }

		/// <summary>
		/// Apparently Stripe can email a receipt to the customer? Pretty weird. Do not use!
		/// </summary>
		[JsonProperty (PropertyName = "receipt_email")]
		public string ReceiptEmailAddress { get; set; }
	}
}
