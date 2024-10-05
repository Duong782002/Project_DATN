using NK.Core.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace NK.Core.Model.Entities
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AddressName { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public Status Status { get; set; } = Status.ACTIVE;
        public Role Roles { get; set; }
        public bool IsFirst { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public virtual List<Order>? Orders { get; set; }
        public virtual List<Contract>? Contracts { get; set; }
        public virtual IEnumerable<ShoppingCartItems>? ShoppingCartItems { get; set; }
        public virtual IEnumerable<Address>? Addresses { get; set; }
    }
}
