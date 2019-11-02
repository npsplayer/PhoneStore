using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("CUSTOMER")]
    public class Customer
    {
        
        public int CustomerID { get; set; }

        public int? UserID { get; set; }

        public User User { get; set; }
        
        public string FirstName { get; set; }

        
        public string SecondName { get; set; }

        
        public string Patronymic { get; set; }

        
        public string Email { get; set; }

        
        public DateTime DateOfBirth { get; set; }

        
        public string PhoneNumber { get; set; }

        
        public int? AddressID { get; set; }

        public virtual Address Address { get; set; }
        

        
    }
}
