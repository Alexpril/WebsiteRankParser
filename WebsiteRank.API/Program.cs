using Microsoft.EntityFrameworkCore;
using Radzen;
using Microsoft.Identity.Web.UI;
using WebsiteRank.Core.Repository;
using WebsiteRank.API.Components;
using WebsiteRank.Core.Services;
using WebsiteRank.Core.Constants;
using WebsiteRank.DAL.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

// Configure Entity Framework with SQL Server connection string
builder.Services.AddDbContext<WebsiteRankDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), 
    contextLifetime: ServiceLifetime.Transient);

builder.Services.AddTransient<IWebsiteRankRepository, WebsiteRankRepository>();
builder.Services.AddScoped<IWebsiteRankService, WebsiteRankService>();
builder.Services.AddScoped<IGoogleService, GoogleService>();

builder.Services.AddHttpClient(WebsiteRankConstants.GoogleHttpClientName, client =>
{
    client.BaseAddress = new Uri("https://www.google.com/");

    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
    client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

await app.RunAsync();
