using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("REVIEW")]
    public class Review
    {
        public int ReviewID { get; set; }

        public int? ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public float Rating { get; set; }

        public DateTime Date { get; set; }
    }
}
