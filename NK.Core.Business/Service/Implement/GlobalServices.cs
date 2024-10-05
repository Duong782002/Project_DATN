using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public class GlobalServices : IGlobalServices
    {
        public AppUser? CurrentUser { get; set; }
        public bool IsFirst { get; set; }
    }
}
