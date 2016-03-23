using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Networking;

namespace Brass9.Clients.Stripe.Accounts
{
	public static class StripeClientExtensions
	{
		/// <summary>
		/// This method has no args; you simply ask to make a Customer and Stripe gives you back an ID to work with,
		/// as part of a Customer object.
		/// </summary>
		/// <param name="_this"></param>
		/// <returns></returns>
		public static async Task<StripeResponse<Customer>> AddCustomerAsync(this StripeClient _this)
		{
			var custResponse = await _this.CallAsync<Customer>(Customer.UrlPath);
			return custResponse;
		}

		public static async Task<StripeResponse<Card>> AddCardAsync(this StripeClient _this, CardInputForCard card)
		{
			// https://stripe.com/docs/api#create_card
			string url = Customer.UrlPath + "/" + card.CustomerId + "/sources";

			var cardRe = await _this.CallAsync<CardInputForCard, Card>(url, card);
			return cardRe;
		}

		public static async Task<StripeResponse<Card>> AddCardAsync(this StripeClient _this, CardInputForToken card)
		{
			// https://stripe.com/docs/api#create_card
			string url = Customer.UrlPath + "/" + card.CustomerId + "/sources";

			var cardRe = await _this.CallAsync<CardInputForToken, Card>(url, card);
			return cardRe;
		}
	}
}
