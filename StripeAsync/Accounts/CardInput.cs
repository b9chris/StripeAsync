using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Accounts
{
	// https://stripe.com/docs/api#create_card
	public class CardInput
	{
		/// <summary>
		/// Set in constructor. Always "card".
		/// </summary>
		[JsonProperty("object")]
		public string PaymentType { get; set; }

		/// <summary>
		/// Required.
		/// </summary>
		[JsonProperty("number")]
		public string Number { get; set; }

		/// <summary>
		/// Required. 2 digits. Card expiration month.
		/// </summary>
		[JsonProperty("exp_month")]
		public string Month { get; set; }

		/// <summary>
		/// Required. 2 or 4 digits. Card expiration year.
		/// </summary>
		[JsonProperty("exp_year")]
		public string Year { get; set; }

		// Not in use. For when a debit card is in use on a Stripe account, and you want to send payments via Stripe to
		// it. If you do set one via the API, you need to be pointing to a Stripe account (not supported in this lib)
		// and it needs to be a US debit card in USD - so the only valid value here would be "usd".
		//[JsonProperty("currency")]
		//public string Currency { get; set; }

		/// <summary>
		/// Highly recommended. Optional.
		/// </summary>
		[JsonProperty("cvc")]
		public string Cvc { get; set; }

		/// <summary>
		/// Optional. Not even recommended.
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("address_line1")]
		public string Address1 { get; set; }

		[JsonProperty("address_line2")]
		public string Address2 { get; set; }

		[JsonProperty("address_city")]
		public string City { get; set; }

		[JsonProperty("address_state")]
		public string State { get; set; }

		/// <summary>
		/// Optional. Not even recommended.
		/// </summary>
		[JsonProperty("address_zip")]
		public string Zip { get; set; }

		[JsonProperty("address_country")]
		public string Country { get; set; }



		public CardInput()
		{
			PaymentType = "card";
		}
	}
}
