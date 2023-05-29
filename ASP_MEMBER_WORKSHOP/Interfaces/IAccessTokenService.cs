using ASP_MEMBER_WORKSHOP.Entitiy;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_MEMBER_WORKSHOP.Interfaces
{
    internal interface IAccessTokenService
    {
        string GenerateAccessToken(string email, int minut = 60);

        Members VerifyAccessToken(string accessToken);

        //Member VerifyAccessToken(string accessToken);
    }
}
