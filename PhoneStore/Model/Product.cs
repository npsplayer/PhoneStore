using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("PRODUCT")]
    public class Product
    {
        public int ProductID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Manufacturer { get; set; }

        
        public double Price { get; set; }

        public byte[] Photo { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ProductComparison> ProductComparisons { get; set; }
        public Product()
        {
            ProductOptions = new List<ProductOption>();
            Baskets = new List<Basket>();
            OrderHistories = new List<OrderHistory>();
            Reviews = new List<Review>();
            Favorites = new List<Favorite>();
            ProductComparisons = new List<ProductComparison>();
        }

    }
}
