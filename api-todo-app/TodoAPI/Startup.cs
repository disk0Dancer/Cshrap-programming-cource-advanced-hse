using System.Configuration;
using Microsoft.EntityFrameworkCore;
namespace TodoAPI;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {

        string connectionString = "server=localhost;database=ToDoDB;user=root";
        services.AddDbContext<ToDoDbContext>(op
            =>op.UseSqlServer(connectionString)); 
        services.AddControllersWithViews();
    }

    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


}