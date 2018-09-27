using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Antiforgery.Internal;

namespace ManagementService.Web.Controllers
{
    public interface IAntiForgeryCookieService
    {
        string RegenerateAntiForgeryCookies(ClaimsPrincipal claims);
        void DeleteAntiForgeryCookies();
        string RefreshAntiForgeryCoockie();
    }
    

    public class AntiForgeryCookieService : IAntiForgeryCookieService
    {
        private const string XsrfTokenKey = "XSRF-TOKEN";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAntiforgery _antiforgery;
        private readonly IOptions<AntiforgeryOptions> _antiforgeryOptions;

        public AntiForgeryCookieService(
            IHttpContextAccessor contextAccessor,
            IAntiforgery antiforgery,
            IOptions<AntiforgeryOptions> antiforgeryOptions)
        {
            _contextAccessor = contextAccessor;

            _antiforgery = antiforgery;

            _antiforgeryOptions = antiforgeryOptions;
        }

        public string RegenerateAntiForgeryCookies(ClaimsPrincipal claims)
        {
            var httpContext = _contextAccessor.HttpContext;
            httpContext.User = claims;
            //httpContext.SignInAsync(claims);

            //var antiforgeryFeature = .Features.Get<IAntiforgeryFeature>();
            //antiforgeryFeature.CookieToken = null;
            //antiforgeryFeature.HaveGeneratedNewCookieToken = false;
            //antiforgeryFeature.HaveDeserializedCookieToken = false;
            //antiforgeryFeature.NewRequestTokenString = null;
            
            var tokens = _antiforgery.GetAndStoreTokens(httpContext);
           // DeleteAntiForgeryCookies();
            httpContext.Response.Cookies.Append(
                 key: XsrfTokenKey,
                  value: tokens.RequestToken,
                  options: new CookieOptions
                  {
                      HttpOnly = false // Now JavaScript is able to read the cookie
                  });
            return tokens.RequestToken;
        }

        public string RefreshAntiForgeryCoockie()
        {
            var httpContext = _contextAccessor.HttpContext;
            var tokens = _antiforgery.GetAndStoreTokens(httpContext);
            // DeleteAntiForgeryCookies();
            httpContext.Response.Cookies.Append(
                 key: XsrfTokenKey,
                  value: tokens.RequestToken,
                  options: new CookieOptions
                  {
                      HttpOnly = false // Now JavaScript is able to read the cookie
                  });
            return tokens.RequestToken;
        }

        public void DeleteAntiForgeryCookies()
        {
            var cookies = _contextAccessor.HttpContext.Response.Cookies;
            cookies.Delete(_antiforgeryOptions.Value.Cookie.Name);
            cookies.Delete(XsrfTokenKey);
        }
    }
}
