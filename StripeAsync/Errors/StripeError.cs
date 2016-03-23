using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Errors
{
	public class StripeError
	{
		[JsonProperty("type")]
		public StripeErrorType ErrorType { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
		[JsonProperty("code")]
		public string Code { get; set; }
		[JsonProperty("param")]
		public string Param { get; set; }
	}
}
