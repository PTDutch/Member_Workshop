using ASP_MEMBER_WORKSHOP.Interfaces;
using ASP_MEMBER_WORKSHOP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ASP_MEMBER_WORKSHOP
{
    public class AuthenticationHandler : DelegatingHandler
    {

        private IAccessTokenService accessTokenService;
        public AuthenticationHandler()
        {
            this.accessTokenService = new DBAccessTokenService();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var Authorization = request.Headers.Authorization;
            if (Authorization != null)
            {
                string AccessToken = Authorization.Parameter;
                string AccessTokenType = Authorization.Scheme;
                if(AccessTokenType.Equals("Bearer"))
                {
                    var UserLogin = this.accessTokenService.VerifyAccessToken(AccessToken);
                    var UserLogin1 = this.accessTokenService.VerifyAccessToken(AccessToken);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}