
namespace CoreoHome.ProcessBulkUploadFiles
{
    public abstract class Setting
    {
        public const string PatientUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/BulkInvitePatient";
        public const string CareTeamUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/CareTeam";
        public const string ServiceProviderUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/ServiceProvider";
        public const string EntityUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/Entity";
        public const string EntityServiceProviderUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/EntityServiceProvider";
        public const string EntityUserUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/EntityUser";
        public const string PatientTagsUrl = "https://chqa-ct-api.coreoflowsandbox.com/api/CareTeam/BulkUpload/PatientTags";
    }
}
