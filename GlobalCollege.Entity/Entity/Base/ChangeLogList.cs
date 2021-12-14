using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public class ChangeLogList
    {
        public ChangeLogList()
        {
            ChangeLogs = new List<RecordChangeLog>();
        }

        public List<RecordChangeLog> ChangeLogs { get; set; }

    }

    public class RecordChangeLog
    {
        public RecordChangeLog()
        {
            PropertyChangeLogs = new List<PropertyChangeLog>();
        }
        public DateTime ChangeDate { get; set; }
        public RecordStatus ChangeStatus { get; set; }
        public int ModificationNumber { get; set; }
        public List<PropertyChangeLog> PropertyChangeLogs { get; set; }
    }

    public class PropertyChangeLog
    {
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
