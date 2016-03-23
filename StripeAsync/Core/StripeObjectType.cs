using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Brass9.Clients.Stripe.Core.Serialization;

namespace Brass9.Clients.Stripe.Core
{
    [JsonConverter (typeof (StripeEnumConverter<StripeObjectType>))]
    public enum StripeObjectType {
        Unknown,
        Account,
        Balance,
        BalanceTransaction,
        Card,
        Charge,
        Coupon,
        Customer,
        Discount,
        Dispute,
        Event,
        InvoiceItem,
        Invoice,
        LineItem,
        Plan,
        Subscription,
        Token,
        Transfer,
    }
}
