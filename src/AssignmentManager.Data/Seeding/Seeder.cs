using System;
using System.Linq;

using AssignmentManager.Data.Models;

namespace AssignmentManager.Data.Seeding
{
    public class Seeder : ISeeder
    {
        public void Seed(AssignmentManagerDbContext context, IServiceProvider serviceProvider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            if (!context.Colors.Any())
            {
                context.Colors.AddRange(
                    new Color() { Name = "#F94144" },
                    new Color() { Name = "#F9844A" },
                    new Color() { Name = "#F8961E" },
                    new Color() { Name = "#F9C74F" },
                    new Color() { Name = "#90BE6D" },
                    new Color() { Name = "#80B376" },
                    new Color() { Name = "#4D908E" },
                    new Color() { Name = "#577590" },
                    new Color() { Name = "#3F7999" },
                    new Color() { Name = "#277DA1" },
                    new Color() { Name = "#1B9AAA" },
                    new Color() { Name = "#07004D" },
                    new Color() { Name = "#9B7EDE" },
                    new Color() { Name = "#8338EC" },
                    new Color() { Name = "#A846A0" },
                    new Color() { Name = "#FFD2FC" });

                context.SaveChanges();
            }
        }
    }
}
