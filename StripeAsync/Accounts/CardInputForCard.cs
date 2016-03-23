using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Accounts
{
	/// <summary>
	/// Use when adding a Card, with CC info, to a Customer, or invoking a card in a Charge.
	/// </summary>
	public class CardInputForCard
	{
		/// <summary>
		/// Required. The Customer to add this Card to. Not serialized; emits in the URL the signal is POSTed to.
		/// </summary>
		[JsonIgnore]
		public string CustomerId { get; set; }

		/// <summary>
		/// Required. Serializes as source.
		/// </summary>
		[JsonProperty (PropertyName = "source")]
		public CardInput Card { get; set; }
	}
}
