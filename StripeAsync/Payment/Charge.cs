using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Accounts;
using Brass9.Clients.Stripe.Core;
using Brass9.Clients.Stripe.Core.Serialization;

namespace Brass9.Clients.Stripe.Payment
{
	[JsonObject (MemberSerialization.OptIn)]
	public class Charge : StripeObjectWithId
	{
		public const string UrlPath = "charges";

	
		
		[JsonProperty (PropertyName = "attempted")]
		public bool Attempted { get; set; }
		[JsonProperty (PropertyName = "refunded")]
		public bool Refunded { get; set; }
		[JsonProperty (PropertyName = "amount_refunded")]
		public int AmountRefunded { get; set; }
		[JsonProperty (PropertyName = "paid")]
		public bool Paid { get; set; }
		[JsonProperty (PropertyName = "amount")]
		public int Amount { get; set; }
		[JsonProperty (PropertyName = "fee")]
		public int Fee { get; set; }
		[JsonProperty (PropertyName = "livemode")]
		public bool LiveMode { get; set; }
		[JsonProperty (PropertyName = "dispute")]
		public Dispute Dispute { get; set; }
		[JsonProperty (PropertyName = "description")]
		public string Description { get; set; }
		[JsonProperty (PropertyName = "currency")]
		public string Currency { get; set; }
		[JsonProperty (PropertyName = "created")]
		[JsonConverter (typeof (UnixDateTimeConverter))]
		public DateTime? Created { get; set; }
		[JsonProperty (PropertyName = "card")]
		public Card Card { get; set; }
		[JsonProperty (PropertyName = "customer")]
		public string Customer { get; set; }
		[JsonProperty (PropertyName = "fee_details")]
		public FeeDetail [] FeeDetails { get; set; }
		[JsonProperty (PropertyName = "failure_message")]
		public string FailureMessage { get; set; }
		[JsonProperty (PropertyName = "invoice")]
		public string Invoice { get; set; }
	}
}
