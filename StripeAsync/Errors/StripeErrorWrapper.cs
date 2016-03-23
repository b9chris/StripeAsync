using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Errors
{
	public class StripeErrorWrapper
	{
		[JsonProperty("error")]
		public StripeError Error { get; set; }
	}
}
