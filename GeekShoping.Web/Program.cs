using GeekShoping.Web.Services;
using GeekShoping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IProductService, ProductService>(c => 
    c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
);

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";

})
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
        {
            options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ClientId = "geek_shoping";
            options.ClientSecret = "bankaisenbonzakurakageyoshi";
            options.ResponseType = "code";
            options.ClaimActions.MapJsonKey("role", "role", "role");
            options.ClaimActions.MapJsonKey("sub", "sub", "sub");
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "role";
            options.Scope.Add("geek_shoping");
            options.SaveTokens = true;
            options.CallbackPath = "/signin-oidc";
            options.Backchannel = new HttpClient 
            {
                BaseAddress = new Uri(builder.Configuration["ServiceUrls:IdentityServer"])
            };
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
