using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public class FileSetup : BaseEntity<Guid>
    {
        public string AllowedExtesion { get; set; }
        
    }
}
