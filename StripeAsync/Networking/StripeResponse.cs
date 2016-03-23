using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Brass9.Clients.Stripe.Errors;

namespace Brass9.Clients.Stripe.Networking
{
	public class StripeResponse<T>
	{
		public HttpResponseMessage Response;
		public T Result;
		public StripeError Error;
	}
}
