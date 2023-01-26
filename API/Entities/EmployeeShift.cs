
namespace API.Entities
{
    public class EmployeeShift
    {
        public int id { get; set; }
        public Employee Employee { get; set; }
        public Shift Shift { get; set; }
    }
}