using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Dotnet_frameworks_project.Seeders;
using Dotnet_frameworks_project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection"); builder.Services.AddDbContext<ApplicationContext>(options =>
     options.UseSqlServer(connectionString)); builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddMvc()
       .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
       .AddDataAnnotationsLocalization();


builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews();

// for app controller 

builder.Services.AddHttpContextAccessor();

// for app controller
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // lockout settings

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // ApplicationUser settings

    options.User.RequireUniqueEmail = false;
});

//builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
//builder.Services.Configure<MailKitOptions>(options =>
//{
//    options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
//    options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
//    options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
//    options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
//    options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
//    options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];

//    // Set it to TRUE to enable ssl or tls, FALSE otherwise
//    options.Security = false;  // true zet ssl or tls aan
//});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
//voor de seeder 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    SeedDatabase.Initialize(services, userManager);
}

//voor de seeder
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture("nl-BE")
     .AddSupportedCultures(Language.SupportedLanguages)
     .AddSupportedUICultures(Language.SupportedLanguages);

app.UseRequestLocalization(localizationOptions);


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseMiddleware<SessionUser>();
app.MapRazorPages();
app.Run();
