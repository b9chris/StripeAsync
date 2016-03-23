using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Accounts
{
	/// <summary>
	/// Use when tying an existing Card you have a token for to a customer.
	/// </summary>
	public class CardInputForToken
	{
		[JsonIgnore]
		public string CustomerId { get; set; }

		/// <summary>
		/// Card token id
		/// </summary>
		[JsonProperty (PropertyName = "source")]
		public string Source { get; set; }
	}
}
