using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Employee
{
    public class EmployeeCreate
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Gender Gender { get;set; }
        public Status Status { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string AddressName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }   

    }
}
