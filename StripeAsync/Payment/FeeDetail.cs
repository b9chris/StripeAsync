using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brass9.Clients.Stripe.Core;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	[JsonObject (MemberSerialization.OptIn)]
	public class FeeDetail
	{
		[JsonProperty (PropertyName = "type")]
		string Type { get; set; }
		[JsonProperty (PropertyName = "currency")]
		string Currency { get; set; }
		[JsonProperty (PropertyName = "application")]
		string Application { get; set; }
		[JsonProperty (PropertyName = "description")]
		string Description { get; set; }
		[JsonProperty (PropertyName = "amount")]
		public int Amount { get; set; }
	}
}
