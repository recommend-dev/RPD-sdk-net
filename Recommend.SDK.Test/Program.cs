using Recommend.SDK;

// initialize SDK with your API key and optional API URL
Recommend.SDK.APIClient client = new APIClient("your-api-key-here");

// this call is used to test connection, in PROD can be removed. Returns true if connection parameters are ok, false if there is an issue with code.
Console.WriteLine("Connection test result: " + await client.TestConnection());

 // generate conversion when purchase occurs - send rcmndref code. Returns response with status code 200 if connection and referral code are ok
var referralCheck = await client.ReferralCheck("valid-rcmdn-code");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(referralCheck) );

// invalid code or wrong API endpoint URL will return null response
var badReferralCheck = await client.ReferralCheck("invalid-rcmdn-code");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(badReferralCheck) );