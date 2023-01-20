using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Employee Manager { get; set; }
    }
}