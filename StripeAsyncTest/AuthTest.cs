using System;
using System.Net;
using System.Threading.Tasks;
using Brass9.Clients.Stripe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StripeTest
{
	[TestClass]
	public class AuthTest
	{
		[TestMethod]
		public async Task TestAuth()
		{
			string url = "https://api.stripe.com/v1/charges";
			var authReq = HttpWebRequest.Create(url);

			// http://stackoverflow.com/a/3198224/176877
			var credentialCache = new CredentialCache();
			credentialCache.Add(new Uri(url), "Basic", new NetworkCredential("sk_test_YOURKEYHERE", ""));
			authReq.Credentials = credentialCache;

			var re = await authReq.GetResponseAsync();
		}
	}
}
