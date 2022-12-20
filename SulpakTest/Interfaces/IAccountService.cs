using SulpakTest.Models;

namespace SulpakTest.Interfaces
{
    public interface IAccountService
    {
        Task Seed();
        Task<User> GetCurrentUser();
        Task<bool> TryLogin(LoginUser model);
        Task Logout();
    }
}
