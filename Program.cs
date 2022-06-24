using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using MovieProDemo.Data;
using MovieProDemo.Models.Settings;
using MovieProDemo.Services;
using MovieProDemo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(ConnectionService.GetConnectionString(builder.Configuration)));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register new service: Configures an instance of AppSettings against appsettings.json file
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Register other services
builder.Services.AddTransient<SeedService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRemoteMovieService, TMDBMovieService>();
builder.Services.AddScoped<IDataMappingService, TMDBMappingService>();
builder.Services.AddSingleton<IImageService, BasicImageService>();

var app = builder.Build();

var dataService = app.Services
                     .CreateScope()
                     .ServiceProvider
                     .GetRequiredService<SeedService>();

await dataService.ManageDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
