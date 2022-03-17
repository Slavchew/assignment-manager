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
                    new Color() { Name = "#F97838" },
                    new Color() { Name = "#F99A4B" },
                    new Color() { Name = "#F9C74F" },
                    new Color() { Name = "#1C6446" },
                    new Color() { Name = "#438253" },
                    new Color() { Name = "#80B376" },
                    new Color() { Name = "#9FD178" },
                    new Color() { Name = "#1053B2" },
                    new Color() { Name = "#277DA1" },
                    new Color() { Name = "#4FBFCD" },
                    new Color() { Name = "#7AC7FD" },
                    new Color() { Name = "#8338EC" },
                    new Color() { Name = "#B95DED" },
                    new Color() { Name = "#EE82EE" },
                    new Color() { Name = "#FFD2FC" });

                context.SaveChanges();
            }
        }
    }
}
