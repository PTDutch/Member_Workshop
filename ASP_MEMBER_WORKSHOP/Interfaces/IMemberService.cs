using ASP_MEMBER_WORKSHOP.Entitiy;
using ASP_MEMBER_WORKSHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_MEMBER_WORKSHOP.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<Members> MemberItems { get; }
        void UpdateProfile(string email, ProfileModel model);
    }
}
