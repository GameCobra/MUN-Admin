using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MUNAdmin.Data;
using MUNAdmin.Models.LoginModels;
using MUNAdmin.Policies;
using MUNAdmin.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MUNAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MUNAdminContext") ?? throw new InvalidOperationException("Connection string 'MUNAdminContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/SelectLoginMethod"; // redirect if not logged in
    });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("IsAdminOfMUN", policy =>
        policy.Requirements.Add(new IsAdminOfMUN()));

    option.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim(LoginClaims.Role, LoginClaims.AdminRole));

    option.AddPolicy("DelegateOnly", policy =>
        policy.RequireClaim(LoginClaims.Role, LoginClaims.DelegationRole));
});

builder.Services.AddSingleton<IAuthorizationHandler, IsAdminOfMUNHandler>();

builder.Services.AddScoped<UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
