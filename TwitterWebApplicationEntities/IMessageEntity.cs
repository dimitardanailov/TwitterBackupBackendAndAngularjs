using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWebApplicationEntities
{
    interface IMessageEntity
    {
        string UserID { get; set; }
        string Text { get; set; }
        ApplicationUser User { get; set; }
    }
}
