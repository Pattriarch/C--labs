using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor;
using Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IAuthorsProvider, AuthorsProvider>();
builder.Services.AddScoped<IBooksProvider, BooksProvider>();
builder.Services.AddScoped<ICategoriesProvider, CategoriesProvider>();
builder.Services.AddScoped<IPublishersProvider, PublishersProvider>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7104") });

await builder.Build().RunAsync();
