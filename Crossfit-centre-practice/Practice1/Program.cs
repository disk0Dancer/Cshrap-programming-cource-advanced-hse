using Korzh.DbUtils;
using Practice1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PracticeCrossfitContext>(options => {
    DbInitializer.Create(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
    });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Login}");
    endpoints.MapRazorPages();
});


app.Run();

// var u = new User();
// var u1 = new User();
// var s = "password";
// u.PasswordHash = "password";
// u1.PasswordHash = "password";
// Console.WriteLine(u.PasswordHash);
// Console.WriteLine(u1.PasswordHash);
// Console.WriteLine(u.VerifyPassword("password"));
// Console.WriteLine(u.VerifyPassword(s));
//
// Console.WriteLine(u1.VerifyPassword("password"));
// Console.WriteLine(u1.VerifyPassword(s));


