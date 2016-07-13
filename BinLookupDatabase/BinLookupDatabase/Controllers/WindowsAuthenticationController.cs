using System.Web.Http;
using BinLookupDatabase.Repository.Interface;

namespace BinLookupDatabase.Controllers
{
    public class WindowsAuthenticationController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public WindowsAuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IHttpActionResult Get()
        {
            string user = _userRepository.GetUser();
            if (user != null)
                return Ok(user);

            return Unauthorized();
        }
    }
}
