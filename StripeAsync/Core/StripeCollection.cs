using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Core
{
	[JsonObject (MemberSerialization.OptIn)]
	public class StripeCollection<T> : IEnumerable<T>
	{
		[JsonProperty ("url")]
		public string Url { get; set; }

		[JsonProperty ("count")]
		public int Total { get; set; }

		[JsonProperty ("data")]
		public List<T> Data { get; set; }

		#region IEnumerable[T] implementation
		public IEnumerator<T> GetEnumerator ()
		{
			return Data.GetEnumerator ();
		}
		#endregion

		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}
		#endregion
	}
}
