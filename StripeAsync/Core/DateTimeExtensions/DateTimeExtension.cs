using System;

namespace Brass9.Clients.Stripe.Core.DateTimeExtensions
{
	public static class DateTimeExtension
	{
		static DateTime epoch = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime FromUnixEpoch (this int utc_unix)
		{
			return epoch.AddSeconds (utc_unix);
		}

		public static DateTime FromUnixEpoch (this long utc_unix)
		{
			return epoch.AddSeconds (utc_unix);
		}

		public static long ToUnixEpoch (this DateTime dt)
		{
			dt = dt.ToUniversalTime ();
			return Convert.ToInt64 ((dt - epoch).TotalSeconds);
		}
	}
}
