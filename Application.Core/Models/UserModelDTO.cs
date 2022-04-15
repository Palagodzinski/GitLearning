using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Models
{
    public class UserModelDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
