using Blazor.SubtleCrypto;
using SulpakTest.Interfaces;
using SulpakTest.Models;

namespace SulpakTest.Services
{
    public class AccountService : IAccountService
    {
        private IStorageService _storageService;
        private ICryptoService _crypto;

        public AccountService(IStorageService storageService, ICryptoService crypto)
        {
            _storageService = storageService;
            _crypto = crypto;
        }

        public async Task Seed()
        {
            var _users = new List<User>
            {
                new User{Login = "admin", Password = "123", Role = "admin"},
                new User{Login = "user", Password = "123", Role = "user"},
                new User{Login = "norole", Password = "123", Role = ""},
            };

            foreach (var user in _users)
            {
                var encrypted = await _crypto.EncryptAsync(user);
                await _storageService.SetItem(user.Login, encrypted.Value);
            }
        }

        public async Task<User> GetCurrentUser()
        {
            var login = await _storageService.GetItem<string>(Constants.LocalStorageCurrentUserKey);

            return await _storageService.GetEncryptedItem<User>(login);
        }

        public async Task<bool> TryLogin(LoginUser model)
        {
            var existsUser = await _storageService.GetEncryptedItem<User>(model.Username);

            if (existsUser != null && existsUser.Password == model.Password)
            {
                await _storageService.SetItem(Constants.LocalStorageCurrentUserKey, existsUser.Login);
                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _storageService.RemoveItem(Constants.LocalStorageCurrentUserKey);

        }
    }
}
