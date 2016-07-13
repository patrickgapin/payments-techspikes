using System.Web;
using BinLookupDatabase.Repository.Interface;

namespace BinLookupDatabase.Repository
{
    public class UserRepository: IUserRepository
    {
        public string GetUser()
        {
            if (HttpContext.Current.Request.LogonUserIdentity == null)
                return null;

            var windowsUser = HttpContext.Current.Request.LogonUserIdentity.Name
                .Replace("PLANETPINNACLE\\", "")
                .Replace("PINNACLESPORTS\\", "");

            if (string.IsNullOrWhiteSpace(windowsUser))
                return null;
            return windowsUser;
        }
    }
}