using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Oracle.ManagedDataAccess.Types;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

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

        public static string setHash(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return "-1";
            }
            else
            {
                var md5 = MD5.Create();
                var HASH = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(HASH);
            }
        }
        
    }
}