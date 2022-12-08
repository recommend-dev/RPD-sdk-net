# Recommend API SDK

This is SDK for connecting with [Recommend, Inc. platform](https://www.recommend.co/). If you need any support contact support@recommend.co

# Using the SDK

First, you need to obtain your API key [here](https://www.recommend.co/partners/api-keys).  When you have obtained your API key, you can use our SDK, either built from source here (from master branch) or using [Nuget package](https://www.nuget.org/packages/Recommend.SDK/1.0.1). We have other integration options too - for details, please check our [developer documentation](https://about.recommend.co/api-docs/).

## Initializing SDK

When you have added package reference, you can initialize the SDK like this:

    Recommend.SDK.APIClient client = new APIClient("your-api-key-here");
If you need to access testing environment, please contact support at email above. Initialization accepts custom URL as parameter and also you can modify exception behavior, by setting`throwExceptions=true` in API constructor. By default, exceptions are handled by library but if you prefer to handle them on your own, you can do it this way, for example for logging purposes.
## Referral check

To send us referral information, when conversion occurs, simply call `await  client.ReferralCheck("valid-rcmdn-code")`.  If your API key is correct and referral code is valid, you will get `true` response from this method call. Exception will be raised (and optionally handled by you) when parameters are incorrect or there is an issue with platform. 
## Approving or rejecting conversion

Conversion can be approved or rejected using our SDK.

    client.ApproveConversion(conversionIdYouReceivedFromReferralCheckResponse);
    client.RejectConversion(conversionIdYouReceivedFromReferralCheckResponse);

## Troubleshooting

To validate your connection to our platform, you can use   `await  client.TestConnection()` method - this validates your API key and returns true if everything is OK. If you run into any problems, don't hesitate to contact support, we will do our best to help you.
