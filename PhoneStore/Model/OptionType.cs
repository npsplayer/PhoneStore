using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("OPTIONTYPE")]
    public class OptionType
    {
        public int OptionTypeID { get; set; }

        [StringLength(50)]
        public string OptionTypeName { get; set; }

        public virtual ICollection<Option> Options { get; set; }

        public OptionType()
        {
            Options = new List<Option>();
        }
    }
}
