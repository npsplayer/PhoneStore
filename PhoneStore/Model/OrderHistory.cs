using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("ORDERHISTORY")]
    public class OrderHistory
    {
        public int OrderHistoryID { get; set; }
        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public int KeyFindProduct { get; set; }
    }
}
