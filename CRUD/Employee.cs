using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD
{
    class Employee
    {
        public int Id { get; set; }
        public int JobTitle_Id { get; set; }
        public int Department_Id { get; set; }
        public int Division_Id { get; set; }
        public int Major_Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string NIK { get; set; }
        public string Email { get; set; }
        public string Religion { get; set; }
        public string NPWP { get; set; }
        public string University { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
