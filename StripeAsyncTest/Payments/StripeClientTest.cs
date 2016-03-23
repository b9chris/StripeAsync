using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe;
using Brass9.Clients.Stripe.Accounts;
using Brass9.Clients.Stripe.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StripeTest.Payments
{
	[TestClass]
	public class StripeClientTest
	{
		[TestMethod]
		public async Task TestChargeAsync()
		{
			var client = new StripeClient();

			var customerRe = await client.AddCustomerAsync();

			 var cardRe = await client.AddCardAsync(new CardInputForCard
			{
				CustomerId = customerRe.Result.Id,
				Card = new CardInput
				{
					Number = "4111111111111111",
					Month = "01",
					Year = "18",
					Cvc = "999",
					Name = "Test Test"
				}
			});

			var chargeRe = await client.AddChargeAsync(new ChargeInputForCustomer
			{
				Customer = customerRe.Result.Id,
				Source = cardRe.Result.Id,
				Amount = 1999,
				Currency = "usd",
				Capture = true,
				ReceiptEmailAddress = "YOUREMAILHERE",
				Description = "Thanks for purchasing.",
				CardStatement = "Testing."
			});
		}
	}
}
