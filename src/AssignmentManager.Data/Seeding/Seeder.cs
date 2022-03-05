using AssignmentManager.Data.Models;
using System;
using System.Linq;

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
                    new Color() { Name = "#FF935B" },
                    new Color() { Name = "#FF72BE" },
                    new Color() { Name = "#AA36E1" },
                    new Color() { Name = "#732537" },
                    new Color() { Name = "#31569C" },
                    new Color() { Name = "#929292" },
                    new Color() { Name = "#CDAD08" },
                    new Color() { Name = "#165239" },
                    new Color() { Name = "#DE6285" },
                    new Color() { Name = "#6949FC" },
                    new Color() { Name = "#BE781F" },
                    new Color() { Name = "#111111" },
                    new Color() { Name = "#63C1FF" },
                    new Color() { Name = "#65E97B" },
                    new Color() { Name = "#FF2222" },
                    new Color() { Name = "#F7E7CE" });

                context.SaveChanges();
            }
        }
    }
}
