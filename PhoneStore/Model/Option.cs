using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("OPTION")]
    public class Option
    {
        public int OptionID { get; set; }
        [StringLength(50)]

        public string OptionName { get; set; }
        public int? OptionTypeID { get; set; }
        public OptionType OptionType { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }

        public Option()
        {
            ProductOptions = new List<ProductOption>();
        }
    }
}
