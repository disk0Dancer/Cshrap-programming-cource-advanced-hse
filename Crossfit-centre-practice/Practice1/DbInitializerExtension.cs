using Korzh.DbUtils;
using Microsoft.EntityFrameworkCore;
using Practice1;
using Practice1.Models;

public static class DbInitializerExtension
{
    public static void EnsureDbInitialized(this IApplicationBuilder app, IConfiguration config, IWebHostEnvironment env)
    {
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<PracticeCrossfitContext>();

        DbInitializer.Create(options =>
        {
            if (context.Database.EnsureCreated())
            {
                DbInitializer.Create(options => { 
                    
                        options.UseSqlServer(config.GetConnectionString("ConnectionString"));
                    })
                    .Seed();
            }
            options.UseSqlServer(config.GetConnectionString("ConnectionString"));
        });
       
    }

    public static async Task Seed(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        
        using (var scope = serviceProvider.CreateScope())
        {
            using var context = scope.ServiceProvider.GetRequiredService<PracticeCrossfitContext>();
            //Seed 
            var userManager = scope.ServiceProvider.GetRequiredService<User>();
            var roleManager = scope.ServiceProvider.GetRequiredService<Role>();
            var genderManager = scope.ServiceProvider.GetRequiredService<Gender>();
            
            await roleManager.CreateAsync(new Role("Посетитель"));
            await roleManager.CreateAsync(new Role("Тренер"));
            
            await genderManager.CreateAsync(new Gender("Мужской"));
            await genderManager.CreateAsync(new Gender("Женский"));

            // creating admin
            var user = new User
            {
                Fio = "Admin Admin Admin",
                Phone = "admin@admin.com",
                Email = "admin@admin.com",
                PasswordHash = "admin",
                BirthDate = new DateTime(1999,11,11),
                RoleId = context.Roles.Where(r=>r.Name=="Тренер").Select(r=>r.Id).FirstOrDefault(),
                GenderId = context.Genders.Where(r=>r.Name=="Мужской").Select(r=>r.Id).FirstOrDefault()
            };
            var userInDb = context.Users
                .Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            if (userInDb == null)
            {
                await userManager.CreateAsync(user);
            }
        }
    }
}

