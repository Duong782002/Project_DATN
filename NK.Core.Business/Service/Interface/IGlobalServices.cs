using NK.Core.Model.Entities;

namespace NK.Core.Business.Service
{
    public interface IGlobalServices
    {
        AppUser? CurrentUser { get; set; }
        bool IsFirst { get; set; }
    }
}
