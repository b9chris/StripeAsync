using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Core;
using Brass9.Clients.Stripe.Core.Serialization;

namespace Brass9.Clients.Stripe.Payment
{
	public class Dispute : StripeObject
	{
		[JsonProperty("status")]
		public DisputeStatus Status { get; set; }

		[JsonProperty ("evidence")]
		public string Evidence { get; set; }

		[JsonProperty ("charge")]
		public string Charge { get; set; }

		[JsonProperty ("created")]
		[JsonConverter (typeof (UnixDateTimeConverter))]
		public DateTime? Created { get; set; }

		[JsonProperty ("currency")]
		public string Currency { get; set; }

		[JsonProperty ("amount")]
		public int Amount;

		[JsonProperty ("livemode")]
		public bool LiveMode { get; set; }

		[JsonProperty ("reason")]
		public DisputeReason Reason { get; set; }

		[JsonProperty ("evidence_due_by")]
		[JsonConverter (typeof (UnixDateTimeConverter))]
		public DateTime? EvidenceDueBy { get; set; }
	}
}
