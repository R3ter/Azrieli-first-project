using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class EmployeeShift
    {
        public int id { get; set; }
        public Employee Employee { get; set; }
        public Shift Shift { get; set; }
    }
}