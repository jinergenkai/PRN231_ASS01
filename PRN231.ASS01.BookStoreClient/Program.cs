using PRN231.ASS01.BookStoreClient.Client;

namespace PRN231.ASS01.BookStoreClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<HttpClient>();
            builder.Services.AddScoped<PublisherClient>();
            builder.Services.AddScoped<UserClient>();
            builder.Services.AddScoped<RoleClient>();
            builder.Services.AddScoped<AuthorClient>();
            builder.Services.AddScoped<BookClient>();
            builder.Services.AddScoped<BookAuthorClient>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}