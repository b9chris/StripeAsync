using System;
using System.Threading.Tasks;
using Brass9.Clients.Stripe;
using Brass9.Clients.Stripe.Accounts;
using Brass9.Clients.Stripe.Core.Serialization;
using Brass9.Clients.Stripe.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StripeTest.Accounts
{
	[TestClass]
	public class StripeClientTest
	{
		[TestMethod]
		public async Task TestAddCustomerAsync()
		{
			var client = new StripeClient();
			var customerRe = await client.AddCustomerAsync();
			Assert.IsNotNull(customerRe.Result);
			Assert.IsTrue(customerRe.Result.Id.StartsWith("cus_"));
		}

		[TestMethod]
		public void TestConvertCardInput()
		{
			var cardInput = new CardInputForCard
			{
				CustomerId = "cus_000000000000",
				Card = new CardInput
				{
					Number = "4111111111111111",
					Month = "01",
					Year = "18",
					Cvc = "999",
					Name = "Test Test"
				}
			};

			var serializer = StripeFormSerializer.O;
			var body = serializer.Serialize(cardInput);

			Assert.AreEqual("source[object]=card&source[number]=4111111111111111&source[exp_month]=01&source[exp_year]=18&source[cvc]=999&source[name]=Test+Test", body);
		}

		[TestMethod]
		public async Task TestError()
		{
			var client = new StripeClient();
			// Attempts to add a card to a customer that doesn't exist
			var cardRe = await client.AddCardAsync(new CardInputForToken
			{
				CustomerId = "cus_000000000000",
				Source = "1234"
			});

			Assert.AreEqual("No such customer: cus_000000000000", cardRe.Error.Message);
			Assert.AreEqual(StripeErrorType.InvalidRequestError, cardRe.Error.ErrorType);
		}

		[TestMethod]
		public async Task TestAddCardAsync()
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
		}
	}
}
