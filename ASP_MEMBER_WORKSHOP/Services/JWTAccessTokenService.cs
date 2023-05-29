using ASP_MEMBER_WORKSHOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jose;
using System.Text;
using Microsoft.Ajax.Utilities;
using ASP_MEMBER_WORKSHOP.Entitiy;

namespace ASP_MEMBER_WORKSHOP.Services
{
    public class JWTAccessTokenService : IAccessTokenService
    {

        private byte[] secretKey = Encoding.UTF8.GetBytes("C# ASP.NET MEMBER  WORKSHOP");

        public string GenerateAccessToken(string email, int minut = 60)
        {
            JWTPayload payload = new JWTPayload
            {
                email = email,
                exp = DateTime.UtcNow.AddMinutes(minut)
            };
            return JWT.Encode(payload, this.secretKey, JwsAlgorithm.HS256);
        }

        //public Member VerifyAccessToken(string accessToken)
        //{
        //    throw new NotImplementedException();
        //}

        public Members VerifyAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }
    }

    public class JWTPayload
    {
        public string email { get; set; }
        public DateTime exp { get; set; }
    }
}