using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Appuser
{
    public class RegisterInput
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get;set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
