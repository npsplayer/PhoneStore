using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("ADDRESS")]
    public class Address
    {

       
        public int AddressID {get; set;}

        
        public string Street { get; set; }

        
        public string HouseNumber { get; set; }

        
        public string Entrance { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public Address()
        {
            Customers = new List<Customer>();
        }

    }
}
