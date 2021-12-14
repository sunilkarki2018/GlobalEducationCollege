using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity.DTO;

namespace GlobalCollege.Entity.DTO
{
    public class ExceptionLoggerDTO : BaseEntityDTO<Guid>
    {
        [Required]
        public string ExceptionMessage { get; set; }
        [StringLength(100)]
        [Required]
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
