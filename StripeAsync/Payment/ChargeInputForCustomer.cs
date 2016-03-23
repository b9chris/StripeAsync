using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	public class ChargeInputForCustomer : BaseChargeInput
	{
		/// <summary>
		/// Required. A Customer to charge.
		/// </summary>
		[JsonProperty (PropertyName = "customer")]
		public string Customer { get; set; }

		/// <summary>
		/// Optional. A Card Id that belongs to the Customer to charge.
		/// If not included, the default card for the Customer will be charged.
		/// </summary>
		[JsonProperty (PropertyName = "source")]
		public string Source { get; set; }
	}
}
