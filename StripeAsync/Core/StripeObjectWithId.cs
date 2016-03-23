using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Core
{
	public class StripeObjectWithId : StripeObject
	{
		[JsonProperty (PropertyName = "id")] 
		public string Id { get; set; }
	}
}
