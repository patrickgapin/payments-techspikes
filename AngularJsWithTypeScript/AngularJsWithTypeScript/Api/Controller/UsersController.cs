using System.Collections.Generic;
using System.Web.Http;
using AngularJsWithTypeScript.Api.Dtos;

namespace AngularJsWithTypeScript.Api.Controller
{
    public class UsersController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new List<UserDto>
            {
                new UserDto{ UserName = "Omid Gerami" },
                new UserDto{ UserName = "Jerry Lewis" }
            });
        }
    }
}