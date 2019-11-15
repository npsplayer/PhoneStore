using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("BASKET")]
    public class Basket
    {
        public int BasketID { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public int Amount { get; set; }
    }
}
