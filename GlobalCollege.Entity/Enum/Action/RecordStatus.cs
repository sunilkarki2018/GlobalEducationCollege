using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity
{
    public enum RecordStatus
    {
        [Display(Name = "Inactive Records")]
        Inactive = 1,
        [Display(Name = "Active Records")]
        Active,
        [Display(Name = "Unauthorized Or Modified Records")]
        Unauthorized,
        [Display(Name = "Reverted Records")]
        Reverted,
        [Display(Name = "Discarded Records")]
        Discarded,
        [Display(Name = "Deleted Records")]
        Deleted,
        [Display(Name = "Closed Records")]
        Closed

    }
}
