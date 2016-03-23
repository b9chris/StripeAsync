using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Accounts;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
	// https://stripe.com/docs/api#create_charge
	public class ChargeInputWithCard : BaseChargeInput
	{
		/// <summary>
		/// A Card to charge. Required if no Customer.
		/// 
		/// TODO: Can also be a string, TokenId, if you want.
		/// </summary>
		[JsonProperty (PropertyName = "source")]
		public Card Source { get; set; }
	}
}
