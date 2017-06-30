using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;


namespace APIFirstProject.Controllers
{
    [RoutePrefix("test")]
    public class TestController : ApiController
    {
        [AllowAnonymous]
        [Route("gettest")]
        [HttpGet]
        public IHttpActionResult GetTest()
        {
            return Json(new { message = "GetTest" });
        }

        [HttpGet]
        [Route("get")]
        // GET: Test
        public IHttpActionResult Get()
        {
            var caller = User as ClaimsPrincipal;
            var subjectClaim = caller.FindFirst("sub");
            if (subjectClaim != null)
            {
                return Json(new
                {
                    message = "OK user",
                    client = caller.FindFirst("client_id").Value,
                    subject = subjectClaim.Value
                });
            }
            return Json(new
            {
                message = "OK computer",
                client = caller.FindFirst("client_id").Value
            });
        }

        [HttpGet]
        [Route("getusertoken")]
        [AllowAnonymous]
        public TokenResponse GetUserToken()
        {
            var client = new TokenClient("http://localhost:5000/connect/token", "carbon", "21B5F798-BE55-42BC-8AA8-0025B903DC3B");
            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
        }
    }
}