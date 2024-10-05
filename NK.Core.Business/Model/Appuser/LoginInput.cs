using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Appuser
{
    public class LoginInput
    {
        public string UserId { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public Role role { get; set; }  
    }
}
