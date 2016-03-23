using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brass9.Clients.Stripe.Core.Serialization;
using Newtonsoft.Json;

namespace Brass9.Clients.Stripe.Payment
{
    /* duplicate, fraudulent, subscription_canceled, product_unacceptable, product_not_received, unrecognized, credit_not_processed, general */
    [JsonConverter (typeof (StripeEnumConverter<DisputeReason>))]
    public enum DisputeReason {
        Duplicate,
        Fraudulent,
        SubscriptionCanceled,
        ProductUnacceptable,
        ProductNotReceived,
        Unrecognized,
        CreditNotProcessed,
        General
    }
}
