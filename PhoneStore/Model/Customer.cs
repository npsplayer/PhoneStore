using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string SecondName { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }
        
        public int? AddressID { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ProductComparison> ProductComparisons { get; set; }

        public Customer()
        {
            Baskets = new List<Basket>();
            OrderHistories = new List<OrderHistory>();
            Reviews = new List<Review>();
            Favorites = new List<Favorite>();
            ProductComparisons = new List<ProductComparison>();
        }

    }
}
