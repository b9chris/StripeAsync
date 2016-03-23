Asp.Net / .Net C# Library for the Stripe Payment API, written entirely in async, with no fake-async-actually-sync methods lurking around. Some parts derived from XamarinStripe.

Written by Brass Nine Design, licensed under the Apache license.

#Usage

Stripe is a big API so the library is written with the assumption you only want certain slices of it in each use of it. For example a class that deals with Payments wants the code for the Charge API, but isn't interested in everything else, like searching through purchase history, returns, etc.

To this end the library provides a single StripeClient:

	var stripe = new StripeClient()

And then calls to Charges or other APIs first require explicitly asking for that set of Class Extension Methods, like:

	using Brass9.Clients.Stripe.Payment
	. . .
	var chargeRe = await stripe.AddChargeAsync(
						new ChargeInputForCard {...
	
The Models that are passed to/from Stripe are broken down into a few categories:

* Inputs
* Outputs
* Responses
* Enums

For example, you pass to AddChargeAsync a ChargeInputForCard, or ChargeInputForCustomer - an Input - and receive a Charge - an output. This copes with the fact that Stripe doesn't provide or permit the same properties in and out for many of its objects in its API.

Many of the Input classes (like `ChargeInputForCard`) and Output classes (like `Charge`) are derived from or similar to [XamarinStripe](https://github.com/xamarin/XamarinStripe), so where the docs are sparse here you can refer to Xamarin or [Stripe's own docs](https://stripe.com/docs/api).


The Stripe API key is either fetched from Web.Config's appSettings key, name StripeApiKey:

	<appSettings>
		<add key="StripeApiKey" value="sk_YOURKEYHERE"/>
	</appSettings>
	
Or, you can pass it in code:

	var stripe = new StripeClient(apiKey);