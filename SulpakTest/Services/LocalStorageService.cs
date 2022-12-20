using Blazor.SubtleCrypto;
using Microsoft.JSInterop;
using SulpakTest.Interfaces;

namespace SulpakTest.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;
        private ICryptoService _crypto;

        public LocalStorageService(IJSRuntime jsRuntime, ICryptoService crypto)
        {
            _jsRuntime = jsRuntime;
            _crypto = crypto;
        }

        public async Task<T> GetEncryptedItem<T>(string key)
        {
            var value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            var decrypted = await _crypto.DecryptAsync<T>(value);

            return decrypted;
        }

        public async Task<T> GetItem<T>(string key)
        {
             return await _jsRuntime.InvokeAsync<T>("localStorage.getItem", key);
        }

        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
