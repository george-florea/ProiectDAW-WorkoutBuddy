using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Common.DTOs
{
    public class ServiceObjectModel
    {
        public ServiceObjectModel()
        {
            Errors = new List<Tuple<string, string>>();
        }

        public CurrentUserDto CurrentUser { get; set; }
        public List<Tuple<string, string>> Errors { get; set; }
    }
}
