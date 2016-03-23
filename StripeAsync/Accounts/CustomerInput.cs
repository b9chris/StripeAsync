using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brass9.Clients.Stripe.Accounts
{
	/// <summary>
	/// Technically we can flesh this out someday if we want to, specifically if we want to create a Customer and
	/// Card in a single call, but until then the call to make a new Customer actually requires 0 input - so this class
	/// can actually be completely empty.
	/// </summary>
	public class CustomerInput
	{
	}
}
