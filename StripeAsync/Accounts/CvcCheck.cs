using System;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Core.Serialization;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Accounts
{
	[JsonConverter (typeof (StripeEnumConverter<CvcCheck>))]
	public enum CvcCheck
	{
		Unknown,
		Pass,
		Fail,
		Unchecked
	}
}
