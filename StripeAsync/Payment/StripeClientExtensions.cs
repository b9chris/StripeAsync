using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Core;
using Brass9.Clients.Stripe.Networking;

namespace Brass9.Clients.Stripe.Payment
{
	public static class StripeClientExtensions
	{
		public static async Task<StripeResponse<Charge>> AddChargeAsync(this StripeClient _this, ChargeInputWithCard charge)
		{
			// To ensure things like CC requests are idempotent.
			// https://stripe.com/docs/api#idempotent_requests
			// Needs to be an HTTP header...


			// https://github.com/xamarin/XamarinStripe/blob/master/XamarinStripe/StripePayment.cs
			var result = await _this.CallAsync<ChargeInputWithCard, Charge>(Charge.UrlPath, charge);

			return result;
		}

		public static async Task<StripeResponse<Charge>> AddChargeAsync(this StripeClient _this, ChargeInputForCustomer charge)
		{
			var result = await _this.CallAsync<ChargeInputForCustomer, Charge>(Charge.UrlPath, charge);
			return result;
		}

		public static async Task<StripeResponse<Charge>> AddChargeAsync(this StripeClient _this, ChargeInputForCard charge)
		{
			var result = await _this.CallAsync<ChargeInputForCard, Charge>(Charge.UrlPath, charge);
			return result;
		}
	}
}
