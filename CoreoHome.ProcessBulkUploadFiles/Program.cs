using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CoreoHome.ProcessBulkUploadFiles
{
    public class Program
    {
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
                CallApiGet(ConfigurationManager.AppSettings["PatientUrl"], UploadFileType.Patient);
                CallApiGet(ConfigurationManager.AppSettings["CareTeamUrl"], UploadFileType.CareTeam);
                CallApiGet(ConfigurationManager.AppSettings["ServiceProviderUrl"], UploadFileType.ServiceProvider);
                CallApiGet(ConfigurationManager.AppSettings["EntityUrl"], UploadFileType.Entity);
                CallApiGet(ConfigurationManager.AppSettings["EntityServiceProviderUrl"], UploadFileType.EntityServiceProvider);
                CallApiGet(ConfigurationManager.AppSettings["EntityUserUrl"], UploadFileType.EntityUser);
                CallApiGet(ConfigurationManager.AppSettings["PatientTagsUrl"], UploadFileType.PatientTags);
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private static void CallApiGet(string apiUrl, UploadFileType fileType)
        {
            try
            {
                string apiResult;
                using (var client = new HttpClient())
                {
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
