using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    [Table("PHONE")]
    public class Phone
    {
        
        public int PhoneID { get; set; }

        
        public string OS { get; set; }

        
        public string Screen { get; set; }

        
        public string Camera { get; set; }

        
        public string Price { get; set; }

        
        public string Processors { get; set; }

        
        public string Battery { get; set; }

    }
}
