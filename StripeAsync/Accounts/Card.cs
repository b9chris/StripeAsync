using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Core;

namespace Brass9.Clients.Stripe.Accounts
{
	[JsonObject (MemberSerialization.OptIn)]
	public class Card : StripeObjectWithId
	{
		[JsonProperty (PropertyName = "type")]
		public string Type { get; set; }
		[JsonProperty (PropertyName = "country")]
		public string Country { get; set; }
		[JsonProperty (PropertyName = "exp_month")]
		public int ExpirationMonth { get; set; }
		[JsonProperty (PropertyName = "exp_year")]
		public int ExpirationYear { get; set; }
		[JsonProperty (PropertyName = "last4")]
		public string Last4 { get; set; }
		[JsonProperty (PropertyName = "cvc_check")]
		public CvcCheck? CvcCheck { get; set; }
		[JsonProperty (PropertyName = "address_country")]
		public string AddressCountry { get; set; }
		[JsonProperty (PropertyName = "address_state")]
		public string AddressState { get; set; }
		[JsonProperty (PropertyName = "address_zip")]
		public string AddressZip { get; set; }
		[JsonProperty (PropertyName = "address_line1")]
		public string AddressLine1 { get; set; }
		[JsonProperty (PropertyName = "address_line2")]
		public string AddressLine2 { get; set; }
		[JsonProperty (PropertyName = "address_zip_check")]
		public string AddressZipCheck { get; set; }
		[JsonProperty (PropertyName = "name")]
		public string Name { get; set; }
		[JsonProperty (PropertyName = "fingerprint")]
		public string Fingerprint { get; set; }
		[JsonProperty (PropertyName = "deleted")]
		public bool Deleted { get; set; }
	}
}
