using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Oracle.ManagedDataAccess.Types;
using System.ComponentModel.DataAnnotations;

namespace PhoneStore.Model
{
    [Table("USERS")]
    public class User
    {
        
        public int UserID { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public User()
        {
            Customers = new List<Customer>();
        }
    }
}