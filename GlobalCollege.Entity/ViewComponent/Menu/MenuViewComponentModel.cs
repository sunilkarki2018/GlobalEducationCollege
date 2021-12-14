using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class MenuViewComponentModel
    {
        public MenuViewComponentModel()
        {
            PhoneNumberList = new List<InstitutionContactSetupDTO>();
            MenuList = new List<MenuSetupDTO>();
        }
        public string LogoLink { get; set; }
        public List<MenuSetupDTO> MenuList { get; set; }
        public List<InstitutionContactSetupDTO> PhoneNumberList { get; set; }
    }
}
