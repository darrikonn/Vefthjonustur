namespace CourseAPI.Entities.Data {
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using CourseAPI.Entities.Models;
    using System;
    using System.Linq;

    /// <summary>
    /// This class represents as a data seeder to the local database.
    /// </summary>
    public static class SeedData {
        /// <summary>
        /// This method initializes the database with some hardcoded data.
        /// </summary>
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var db = new ApplicationDbContext (
                    serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>())) {
                // check if students have been seeded
                if (!db.Students.Any()) {
                    db.Students.AddRange(
                        new Student {
                            SSN = "1234567890",
                            Name = "Herp McDerpsson 1"
                        },
                        new Student {
                            SSN = "1234567891",
                            Name = "Herpina Derpy 1"
                        },
                        new Student {
                            SSN = "1234567892",
                            Name = "Herp McDerpsson 2"
                        },
                        new Student {
                            SSN = "1234567893",
                            Name = "Herpina Derpy 2"
                        },
                        new Student {
                            SSN = "1234567894",
                            Name = "Herp McDerpsson 3"
                        },
                        new Student {
                            SSN = "1234567895",
                            Name = "Herpina Derpy 3"
                        },
                        new Student {
                            SSN = "1234567896",
                            Name = "Herp McDerpsson 4"
                        },
                        new Student {
                            SSN = "1234567897",
                            Name = "Herpina Derpy 4"
                        },
                        new Student {
                            SSN = "1234567898",
                            Name = "Herp McDerpsson 5"
                        },
                        new Student {
                            SSN = "1234567899",
                            Name = "Herpina Derpy 5"
                        }
                    );
                }
                if (!db.CourseTemplates.Any()) {
                    db.CourseTemplates.Add(new CourseTemplate {
                        CourseId = "T-514-VEFT",
                        Name = "Vefþjónustur"
                    });
                }

                db.SaveChanges();
            }
        }
    }
}
