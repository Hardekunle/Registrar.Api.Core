namespace Registrar.Api.Core.Data
{
    public class LoggedInUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public List<string> Roles { get; set;}
    }
}
