using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	public class ChargeInputForCard : BaseChargeInput
	{
		/// <summary>
		/// Required. A Card Token (NOT Card Id) to charge.
		/// </summary>
		[JsonProperty (PropertyName = "source")]
		public string Source { get; set; }
	}
}
