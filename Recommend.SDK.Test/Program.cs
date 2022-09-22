using Recommend.SDK;

Recommend.SDK.APIClient client = new APIClient("your-api-key-here");

Console.WriteLine("Connection test result: " + await client.TestConnection());

var referralCheck = await client.ReferralCheck("valid-rcmdn-code");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(referralCheck) );

var badReferralCheck = await client.ReferralCheck("invalid-rcmdn-code");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(badReferralCheck) );