using NK.Core.Model.Enums;

namespace NK.Core.Business.Model.Employee
{
    public class EmployeeView
    {
        public string Id { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Status Status { get; set; }

    }
}
