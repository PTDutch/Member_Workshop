using ASP_MEMBER_WORKSHOP.Entitiy;
using ASP_MEMBER_WORKSHOP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_MEMBER_WORKSHOP.Models;
using Microsoft.Ajax.Utilities;

namespace ASP_MEMBER_WORKSHOP.Services
{
    public class DBAccessTokenService : IAccessTokenService
    {

        private DatabaseEntities db = new DatabaseEntities();

        public string GenerateAccessToken(string email, int minut = 60)
        {
            try
            {
                var memberItem = this.db.Members.SingleOrDefault(m => m.email.Equals(email));
                if (memberItem == null) throw new Exception("Not found member");
                var accessTokenCreate = new AccessToken
                {
                    token = Guid.NewGuid().ToString(),
                    exprise = DateTime.Now.AddMinutes(minut),
                    memberID = memberItem.id
                };
                this.db.AccessTokens.Add(accessTokenCreate);
                this.db.SaveChanges();
                return accessTokenCreate.token;
            }
            catch(Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public Members VerifyAccessToken(string accessToken)
        {
            try
            {
                var accessTokenItem = this.db.AccessTokens.SingleOrDefault(item => item.token.Equals(accessToken));
                if (accessTokenItem == null) return null;
                if (accessTokenItem.exprise < DateTime.Now) return null;
                return accessTokenItem.Member;
                    
            }
            catch
            {
                return null;
            }
        }
    }
}