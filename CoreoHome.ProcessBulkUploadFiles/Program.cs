using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CoreoHome.ProcessBulkUploadFiles
{
    public class Program
    {
        private static object JsonConvert;

        public enum UploadFileType
        {
            Patient = 63,
            ServiceProvider = 65,
            Entity = 66,
            CareTeam = 67,
            EntityUser = 97,
            EntityServiceProvider = 98,
            PatientTags = 105
        }

        static void Main(string[] args)
        {
            try
            {
                var token = Token();
                if (!string.IsNullOrEmpty(token.Result))
                {                   
                    CallApiGet(Setting.PatientUrl, UploadFileType.Patient, token.Result);
                    CallApiGet(Setting.CareTeamUrl, UploadFileType.CareTeam, token.Result);
                    CallApiGet(Setting.ServiceProviderUrl, UploadFileType.ServiceProvider, token.Result);
                    CallApiGet(Setting.EntityUrl, UploadFileType.Entity, token.Result);
                    CallApiGet(Setting.EntityServiceProviderUrl, UploadFileType.EntityServiceProvider, token.Result);
                    CallApiGet(Setting.EntityUserUrl, UploadFileType.EntityUser, token.Result);
                    CallApiGet(Setting.PatientTagsUrl, UploadFileType.PatientTags, token.Result);
                }
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static async Task<string> Token()
        {
            var clientid = "roclientsp";
            var clientsecret = "coreohomesecret";
            var granttype = "password";
            //var username = "peter.allen@mailinator.com";
            //var password = "Emids@111";

            var username = "tyler.ford@mailinator.com";
            var password = "Emids@000";

            var formContent = new FormUrlEncodedContent(new[]
                        {
                new KeyValuePair<string, string>("client_id", clientid),
                new KeyValuePair<string, string>("client_secret", clientsecret),
                new KeyValuePair<string, string>("grant_type", granttype),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
                   });

            User data;
            using (var client = new HttpClient())
            {

                var response1 = await client.PostAsync("https://chqa-oauth-api.coreoflowsandbox.com/connect/token", formContent);
                var responseBody1 = response1.Content.ReadAsStringAsync();
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(responseBody1.Result);
            }

            return data.access_token;
        }

        private static async void CallApiGet(string apiUrl, UploadFileType fileType, string token)
        {
            try
            {
                string apiResult;

                using (var client = new HttpClient())
                {
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    apiResult = response.IsSuccessStatusCode
                        ? response.Content.ReadAsStringAsync().Result
                        : $"{(int)response.StatusCode} {response.ReasonPhrase}";
                }

                switch (fileType)
                {
                    case UploadFileType.Patient:
                        apiResult = $"Patient Upload Result: {apiResult}";
                        break;
                    case UploadFileType.CareTeam:
                        apiResult = $"CareTeam Upload Result: {apiResult}";
                        break;
                    case UploadFileType.ServiceProvider:
                        apiResult = $"ServiceProvider Upload Result: {apiResult}";
                        break;
                    case UploadFileType.Entity:
                        apiResult = $"Entity Upload Result: {apiResult}";
                        break;
                    case UploadFileType.EntityServiceProvider:
                        apiResult = $"EntityServiceProvider Upload Result: {apiResult}";
                        break;
                    case UploadFileType.EntityUser:
                        apiResult = $"EntityUser Upload Result: {apiResult}";
                        break;
                    case UploadFileType.PatientTags:
                        apiResult = $"PatientTags Upload Result: {apiResult}";
                        break;
                }

                Console.WriteLine(apiResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
