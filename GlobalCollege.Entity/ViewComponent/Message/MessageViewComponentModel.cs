using GlobalCollege.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalCollege.Entity.ViewComponent
{
    public class MessageViewComponentModel
    {
        public MessageViewComponentModel()
        {
            MessageSetupList = new List<MessageSetupDTO>();
        }
        public MessageSetupDTO Message { get; set; }
        public List<MessageSetupDTO> MessageSetupList { get; set; }
        public List<AboutUsSetupDTO> AboutUsList { get; set; }
    }
}
