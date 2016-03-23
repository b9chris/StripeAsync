using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Core;
using Brass9.Clients.Stripe.Core.Serialization;

namespace Brass9.Clients.Stripe.Accounts
{
	[JsonObject (MemberSerialization.OptIn)]
	public class Customer : StripeObjectWithId
	{
		public const string UrlPath = "customers";

		

		[JsonProperty (PropertyName = "livemode")]
		public bool LiveMode { get; set; }

		[JsonProperty (PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty (PropertyName = "created")]
		[JsonConverter (typeof (UnixDateTimeConverter))]
		public DateTime? Created { get; set; }

		[JsonProperty (PropertyName = "deleted")]
		public bool Deleted { get; set; }

		[JsonProperty (PropertyName = "active_card")]
		public Card Card { get; set; }

		[JsonProperty (PropertyName = "account_balance")]
		public int AccountBalance { get; set; }

		[JsonProperty (PropertyName = "deliquent")]
		public bool? Deliquent { get; set; }

		[JsonProperty (PropertyName = "email")]
		public string Email { get; set; }

		/*[JsonProperty (PropertyName = "discount")]
		public StripeDiscount Discount { get; set; }

		[JsonProperty (PropertyName = "subscription")]
		public StripeSubscription Subscription { get; set; }*/

		[JsonProperty (PropertyName = "cards")]
		public StripeCollection<Card> Cards { get; set; }

		[JsonProperty (PropertyName = "default_card")]
		public string DefaultCard { get; set; }
	}
}
