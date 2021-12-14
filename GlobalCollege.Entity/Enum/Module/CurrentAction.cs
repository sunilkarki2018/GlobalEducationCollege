using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public enum CurrentAction
    {
        View = 1,
        Create,
        Edit,
        Delete,
        Revert,
        Authorise,
        AutoAuthorise,
        Download,
        Close,
        Discard
    }
}
