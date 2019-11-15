using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("PRODUCTOPTION")]
    public class ProductOption
    {
        public int ProductOptionID { get; set; }
        public int? ProductID { get; set; }  
        public Product Product { get; set; }
        public int? OptionID { get; set; }
        public Option Option { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
    }
}
