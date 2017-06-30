using APIFirstProject;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

[assembly: OwinStartup(typeof(Startup))]
namespace APIFirstProject
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(new Func<AppFunc, AppFunc>(next => (async env =>
            {
                
                var context = (HttpContextWrapper)env["System.Web.HttpContextBase"];
                var headers = context.Request.Headers.ToString();
                System.Diagnostics.Debug.WriteLine("Begin Request");
                var task=next.Invoke(env);
                await task;//.Wait();
                System.Diagnostics.Debug.WriteLine(WebUtility.UrlDecode(headers));
                System.Diagnostics.Debug.WriteLine("End Request");
            })));

            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions()
            {
                Authority = "http://localhost:5000",
                ValidationMode = ValidationMode.ValidationEndpoint,
                RequiredScopes = new[] { "api1" }
            });
            
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute());
            app.UseWebApi(config);
            
        }
    }
}