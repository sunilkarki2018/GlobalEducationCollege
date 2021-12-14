using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;


namespace GlobalCollege.Entity
{
    public class ApplicationRoleDetailsDTO : BaseEntityDTO<Guid>
    {
        public ApplicationRoleDetailsDTO()
        {
        }
        [Required(ErrorMessage = "ApplicationRoleId is required.")]
        public Guid RoleId { get; set; }
        [Required(ErrorMessage = "Module Name is required.")]
        [StringLength(100)]
        public string ModuleName { get; set; }
        [Required(ErrorMessage = "SubModule Name is required.")]
        [StringLength(100)]
        public string SubModuleName { get; set; }
        [Required(ErrorMessage = "Can View is required.")]
        public bool CanView { get; set; }
        [Required(ErrorMessage = "Can Create is required.")]
        public bool CanCreate { get; set; }
        [Required(ErrorMessage = "Can Edit is required.")]
        public bool CanEdit { get; set; }
        [Required(ErrorMessage = "Can Delete is required.")]
        public bool CanDelete { get; set; }
        [Required(ErrorMessage = "Can Authorize is required.")]
        public bool CanAuthorize { get; set; }
        [Required(ErrorMessage = "Can Discard is required.")]
        public bool CanDiscard { get; set; }
        [Required(ErrorMessage = "Can Download is required.")]
        public bool CanDownload { get; set; }
        [Required(ErrorMessage = "Can Autoauthorise is required.")]
        public bool CanAutoAuthorise { get; set; }

        public virtual ApplicationRoleDTO ApplicationRole { get; set; }
    }
}
