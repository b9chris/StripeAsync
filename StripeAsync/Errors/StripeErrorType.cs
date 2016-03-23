using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Core.Serialization;

namespace Brass9.Clients.Stripe.Errors
{
	[JsonConverter(typeof(StripeEnumConverter<StripeErrorType>))]
	public enum StripeErrorType
	{
		InvalidRequestError = 0,
		ApiError = 1,
		CardError = 2
	}
}
