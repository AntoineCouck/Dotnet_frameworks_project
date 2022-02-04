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
                ApplicationUser ParentAccount1 = null;
                ApplicationUser ParentAccount2 = null;


                context.Database.EnsureCreated();


                if (!context.Language.Any())
                {
                    context.Language.AddRange(
                        new Language() { Id = "-", Name = "-", Culture = "-", SystemLanguage = false },
                        new Language() { Id = "en", Name = "English", Culture = "UK;US", SystemLanguage = true },
                        new Language() { Id = "fr", Name = "français", Culture = "BE;FR", SystemLanguage = true },
                        new Language() { Id = "nl", Name = "Nederlands", Culture = "BE;NL", SystemLanguage = true }
                    );
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {

                    ApplicationUser dummy = new ApplicationUser { Id = "-", Firstname = "-", Lastname = "-", UserName = "-", Email = "?@?.?", LanguageId = "-", AcceptCookies = true };
                    context.Users.Add(dummy);
                    context.SaveChanges();

                    user1 = new ApplicationUser
                    {
                        UserName = "Admin",
                        Firstname = "Antoine",
                        Lastname = "Couck",
                        LanguageId = "nl",
                        AcceptCookies = true,
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(user1, "Student+1");
                    Thread.Sleep(3000);

                    Logopedist = new ApplicationUser
                    {
                        UserName = "Margot_de_logo",
                        Firstname = "Margot",
                        Lastname = "Delo",
                        LanguageId = "nl",
                        AcceptCookies = false,
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true, 
                        
                    };

                    userManager.CreateAsync(Logopedist, "Abc!12345");
                    Thread.Sleep(3000);

                    ParentAccount1 = new ApplicationUser
                    {
                        UserName = "Jamy.Wolf",
                        Firstname = "Jamy",
                        Lastname = "Wolf",
                        LanguageId = "nl",
                        AcceptCookies = false,
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(ParentAccount1, "Abc!12345");
                    Thread.Sleep(3000);

                    ParentAccount2 = new ApplicationUser
                    {
                        UserName = "Melvin.Kat",
                        Firstname = "Melvin",
                        Lastname = "Kat",
                        LanguageId = "nl",
                        AcceptCookies = false,
                        Email = "System.administrator@studentenbeheer.be",
                        EmailConfirmed = true
                    };

                    userManager.CreateAsync(ParentAccount2, "Abc!12345");
                    Thread.Sleep(3000);

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

                if (!context.Insurance.Any())
                {


                    context.Insurance.AddRange(

                    new Insurance
                    {
                        Name = "Liberale mutualiteit",
                        Adres = "Heldenplein 25, 1800 Vilvoorde",
                        PhoneNumber = 027926619

                    },

                    new Insurance
                    {
                        Name = "Socialistische mutualiteit",
                        Adres = "Grote markt 1, 1800 Vilvoorde",
                        PhoneNumber = 0475214788

                    }
                  );

                    context.SaveChanges();
                }


                if (!context.Patient.Any())
                {

                    context.Patient.AddRange(

                        new Patient
                        {
                            FirstName = "Jamy",
                            LastName = "Wolf",
                            Birthday = DateTime.Now,
                            ParentsPhone = "0475214788",
                            UserId = Logopedist.Id,
                            LeftSessions = 4,
                            InsuranceId = "Liberale mutualiteit",
                            AccountId = ParentAccount1.Id,
                            GenderId = 'M'


                        },
                         new Patient
                         {
                             FirstName = "Melvin",
                             LastName = "Kat",
                             Birthday = DateTime.Now,
                             ParentsPhone = "0475214788",
                             UserId = Logopedist.Id,
                             LeftSessions = 4,
                             InsuranceId = "Socialistische mutualiteit",
                             AccountId = ParentAccount2.Id,
                             GenderId = 'F'


                         }


                  );

                    context.SaveChanges();
                }

                if (!context.Test.Any())
                {
                    context.Test.AddRange(

                    new Test
                    {
                        Name = "Rekensprong",
                        Description = "Iets voor te rekenen",
                        Min_age = 7,
                        Max_age = 13,
                        Usage = "Voor kinderen met reken-problemen"
                    },

                    new Test
                    {
                        Name = "Woordenpakket",
                        Description = "Met woorden spelen",
                        Min_age = 6,
                        Max_age = 13,
                        Usage = "Voor taal problemen"
                    }
                 );

                    context.SaveChanges();
                }

                if (!context.PassedTests.Any())
                {

                    context.PassedTests.AddRange(


                        new PassedTests
                        {
                            PatientId = 1,
                            TestId = "Rekensprong"
                        },



                        new PassedTests
                        {
                            PatientId = 2,
                            TestId = "Woordenpakket"
                        }
                  );

                    context.SaveChanges();
                }


                if (!context.FollowUp_type.Any())
                {
                    context.FollowUp_type.AddRange(

                        new FollowUp_type
                        {
                            Name = "Logo dingo",
                            Description = "Heeft logopedie nodig",
                            Number_of_requiredsessions = 175
                        },

                         new FollowUp_type
                         {
                             Name = "Kan niet tellen",
                             Description = "iets met rekenen denk ik",
                             Number_of_requiredsessions = 175
                         }
                  );
                    context.SaveChanges();
                    Thread.Sleep(3000);
                }


                if (!context.FollowUp_patients.Any())
                {


                    context.FollowUp_patients.AddRange(


                        new FollowUp_patients
                        {
                            FollowUpId = "Logo dingo",
                            PatientId = 1
                        },



                        new FollowUp_patients
                        {
                            FollowUpId = "Kan niet tellen",
                            PatientId = 2
                        }


                  );
                }


                if (!context.Roles.Any())
                {

                    context.Roles.AddRange(

                            new IdentityRole { Id = "Logopedist", Name = "Logopedist", NormalizedName = "logopedist" },
                              new IdentityRole { Id = "Mutualiteit", Name = "Mutualiteit", NormalizedName = "mutualiteit" },
                            new IdentityRole { Id = "Parents", Name = "Parents", NormalizedName = "parents" },
                            new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "admin" }

                            );

                    context.SaveChanges();
                }



                if (user1 != null)
                {
                    context.UserRoles.AddRange(

                        new IdentityUserRole<string> { UserId = user1.Id, RoleId = "Admin" },
                        new IdentityUserRole<string> { UserId = Logopedist.Id, RoleId = "Logopedist" }

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


                        if (l.SystemLanguage)
                            Language.SystemLanguages.Add(l);
                        supportedLanguages.Add(l.Id);
                        string[] even = l.Culture.Split(";");
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
