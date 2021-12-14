using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public class ChangeLogDTO
    {
        public string PropertyName { get; set; }
        public Guid EntityPK { get; set; }
        public object Originalvalue { get; set; }
        public object ModifiedValue { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string LogStatus { get; set; }
    }
}
