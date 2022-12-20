using Blazor.SubtleCrypto;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SulpakTest;
using SulpakTest.Helpers;
using SulpakTest.Interfaces;
using SulpakTest.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAccountService, AccountService>()
                .AddScoped<IStorageService, LocalStorageService>()
                .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
                .AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>()
                .AddScoped<FootballWorldCupDataService>();

builder.Services.AddSubtleCrypto(opt => opt.Key = Constants.CryptoKey);
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
   

await builder.Build().RunAsync();
