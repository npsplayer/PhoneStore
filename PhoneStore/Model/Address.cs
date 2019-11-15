using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Street { get; set; }

        [StringLength(50)]
        public string HouseNumber { get; set; }

        [StringLength(50)]
        public string Room { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public Address()
        {
            Customers = new List<Customer>();
        }

    }
}
