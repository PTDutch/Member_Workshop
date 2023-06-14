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

        private DatabaseEntities db = new DatabaseEntities();

        public string GenerateAccessToken(string email, int minut = 60)
        {
            JWTPayload payload = new JWTPayload
            {
                email = email,
                exp = DateTime.UtcNow.AddMinutes(minut)
            };
            return JWT.Encode(payload, this.secretKey, JwsAlgorithm.HS256);
        }

        public Members VerifyAccessToken(string accessToken)
        {
            try
            {
                JWTPayload payload = JWT.Decode<JWTPayload>(accessToken, this.secretKey);
                if (payload == null) return null;
                if (payload.exp < DateTime.UtcNow) return null;
                return this.db.Members.SingleOrDefault(item => item.email.Equals(payload.email));
            }
            catch 
            {
                return null;
            }
        }
    }

    public class JWTPayload
    {
        public string email { get; set; }
        public DateTime exp { get; set; }
    }
}