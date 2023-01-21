namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Manager { get; set; }
        public int ManagerID { get; set; }
    }
}