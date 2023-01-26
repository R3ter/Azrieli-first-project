namespace API.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StartWorkYear { get; set; }
        public int? DepartmentID { get; set; }
        public Department department { get; set; }
    }
}