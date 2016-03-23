using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Networking;
using Brass9.Web.Script.JsonSerialization;
using Brass9.Clients.Stripe.Core;
using Brass9.Clients.Stripe.Core.Serialization;
using Brass9.Clients.Stripe.Errors;

namespace Brass9.Clients.Stripe
{
	// Based partly on
	// https://github.com/xamarin/XamarinStripe/blob/master/XamarinStripe/StripePayment.cs
	// and derived from the Stripe docs,
	// https://stripe.com/docs/api#intro
	public class StripeClient
	{
		public const string WebConfigKey = "StripeApiKey";
		const string endpoint = "https://api.stripe.com/v1/";
		const string userAgent = "Brass9 1.0";


		protected int timeout = 5 * 1000;	// Default to 5 seconds
		protected string apiKey;



		public StripeClient()
		{
			init(
				//(string)HttpContext.Current.Application[WebConfigKey]
				//(string)ConfigurationSettings.AppSettings[WebConfigKey]
				ConfigurationManager.AppSettings[WebConfigKey]
			);
		}
		public StripeClient(string key)
		{
			init(key);
		}
		public StripeClient(string key, int timeout)
		{
			this.timeout = timeout;
			init(key);
		}

		protected void init(string apiKey)
		{
			this.apiKey = apiKey;
		}

		public async Task<StripeResponse<TResult>> CallAsync<TInput, TResult>(string urlPath, TInput input)
		{
			//var kvs = StripeDictionaryFormConverter.O.Convert<TInput>(input);
			//var content = new FormUrlEncodedContent(kvs);
			// TODO: Use something that emits straight to a stream in production so it never becomes a string that
			// chews up more string space.
			// Keep the strings in debug.
			var body = StripeFormSerializer.O.Serialize(input);
			var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

			return await callAsync<TResult>(urlPath, content);
		}
		public async Task<StripeResponse<TResult>> CallAsync<TResult>(string urlPath)
		{
			return await callAsync<TResult>(urlPath, null);
		}

		protected async Task<StripeResponse<TResult>> callAsync<TResult>(string urlPath, ByteArrayContent content)
		{
			using (var httpClient = new HttpClient())
			{
				authHttpClient(httpClient);

				string url = endpoint + urlPath;

				var re = await httpClient.PostAsync(url, content);

				// TODO: Check if failure
				// https://stripe.com/docs/api/java#pagination

				if (re.IsSuccessStatusCode)
				{
					var reStream = await re.Content.ReadAsStreamAsync();
					var result = JsonNetHelper.Create().ReadAndDisposeStream<TResult>(reStream);

#if DEBUG
					var debugBody = re.Content.ReadAsStringAsync();
#endif

					return new StripeResponse<TResult>
					{
						Response = re,
						Result = result
					};
				}

				string reString = await re.Content.ReadAsStringAsync();
				var error = JsonConvert.DeserializeObject<StripeErrorWrapper>(reString);

				return new StripeResponse<TResult>
				{
					Response = re,
					Error = error.Error
				};
			}
		}

		protected void authHttpClient(HttpClient httpClient)
		{
			// Auth the way Stripe likes, using HTTP Basic Authentication
			// http://stackoverflow.com/a/23914662/176877
			var keyBytes = Encoding.ASCII.GetBytes(apiKey);
			var keyBase64 = Convert.ToBase64String(keyBytes);
			var header = new AuthenticationHeaderValue("Basic", keyBase64);
			httpClient.DefaultRequestHeaders.Authorization = header;
		}
	}
}
