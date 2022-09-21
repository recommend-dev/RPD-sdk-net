using Recommend.SDK;

Recommend.SDK.APIClient client = new APIClient("0fe42cff-9151-4125-a2ed-133c6f3e55eb", "https://rpd-api-stage.azurewebsites.net/apikeys", true);

Console.WriteLine("Connection test result: " + await client.TestConnection());

var referralCheck = await client.ReferralCheck("2fb48e28-c83c-4400-9c67-e453c28028a5");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(referralCheck) );

var badReferralCheck = await client.ReferralCheck("2fb48e28-0000-4400-9c67-e453c28028a5");
Console.WriteLine("Referral check: " + System.Text.Json.JsonSerializer.Serialize(badReferralCheck) );