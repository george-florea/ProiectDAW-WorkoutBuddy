using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Account
{
    public class UserInfoModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int PointsNo { get; set; }
        public float CurrentWeight { get; set; }
    }
}
