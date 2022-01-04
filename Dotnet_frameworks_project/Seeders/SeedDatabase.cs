using Dotnet_frameworks_project.Areas.Identity.Data;
using Dotnet_frameworks_project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_frameworks_project.Seeders
{

    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {

                ApplicationUser user1 = null;
                ApplicationUser Logopedist = null;

                context.Database.EnsureCreated();


                if (!context.Language.Any())
                {
                    context.Language.AddRange(
                        new Language() { Id = "-", Name = "-", Cultures = "-", IsSystemLanguage = false },
                        new Language() { Id = "en", Name = "English", Cultures = "UK;US", IsSystemLanguage = true },
                        new Language() { Id = "fr", Name = "français", Cultures = "BE;FR", IsSystemLanguage = true },
                        new Language() { Id = "nl", Name = "Nederlands", Cultures = "BE;NL", IsSystemLanguage = true }
                    );
                    context.SaveChanges();
                }




                if (!context.Users.Any())
                {

                    ApplicationUser dummy = new ApplicationUser { Id = "-", Firstname = "-", Lastname = "-", UserName = "-", Email = "?@?.?", LanguageId = "-" };
                    context.Users.Add(dummy);
                    context.SaveChanges();

                    user1 = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        LanguageId = "nl",
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    //Logopedist = new ApplicationUser
                    //{
                    //    UserName = "Logopedist",
                    //    Firstname = "Margot",
                    //    Lastname = "Delo",
                    //    LanguageId = "fr",
                    //    Email = "System.administrator@studentenbeheer.be",
                    //    EmailConfirmed = true
                    //};


                    userManager.CreateAsync(user1, "Student+1");
                    //userManager.CreateAsync(Logopedist, "Abc!12345");
                }

                if (!context.Gender.Any())
                {
                    context.Gender.AddRange(

                        new Gender
                        {
                            Name = "Female",
                            ID = 'F'
                        },

                        new Gender
                        {
                            Name = "Male",
                            ID = 'M'
                        },

                        new Gender
                        {
                            Name = "Not set",
                            ID = 'N'
                        }



                        );

                    context.SaveChanges();
                }

                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "Logopedist", Name = "Logopedist", NormalizedName = "logopedist" },
                            new IdentityRole { Id = "Parents", Name = "Parents", NormalizedName = "parents" },
                            new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "admin" }

                            );

                    context.SaveChanges();
                }



                if (user1 != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = user1.Id, RoleId = "Admin" }
                        //new IdentityUserRole<string> { UserId = Logopedist.Id, RoleId = "Logopedist" }

                        );

                    context.SaveChanges();
                }





                List<string> supportedLanguages = new List<string>();
                Language.AllLanguages = context.Language.ToList();
                Language.LanguageDictionary = new Dictionary<string, Language>();
                Language.SystemLanguages = new List<Language>();

                supportedLanguages.Add("nl-BE");
                foreach (Language l in Language.AllLanguages)
                {

                    // key not found = ligne en dessous mettre au dessus du if 
                    Language.LanguageDictionary[l.Id] = l;


                    if (l.Id != "-")
                    {

                        if (l.IsSystemLanguage)
                            Language.SystemLanguages.Add(l);
                        supportedLanguages.Add(l.Id);
                        string[] even = l.Cultures.Split(";");
                        foreach (string e in even)
                        {
                            supportedLanguages.Add(l.Id + "-" + e);
                        }
                    }
                }
                Language.SupportedLanguages = supportedLanguages.ToArray();



            }



        }
    }
}
