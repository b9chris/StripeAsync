using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brass9.Clients.Stripe.Core.Serialization;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	/* won, lost, needs_response, under_review */
	[JsonConverter (typeof (StripeEnumConverter<DisputeStatus>))]
	public enum DisputeStatus
	{
		Won,
		Lost,
		NeedsResponse,
		UnderReview
	}
}
