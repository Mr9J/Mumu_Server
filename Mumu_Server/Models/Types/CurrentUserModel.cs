namespace Mumu_Server.Models.Types
{
    public class CurrentUserModel
    {
        public int id { get; set; }
        public string nickname { get; set; } = null!;
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public string role { get; set; } = null!;
    }
}
