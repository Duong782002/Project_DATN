using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Appuser
{
    public class ForgotPassword
    {
        public string UserName { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
