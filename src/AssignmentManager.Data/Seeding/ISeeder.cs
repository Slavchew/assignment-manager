using System;

namespace AssignmentManager.Data.Seeding
{
    public interface ISeeder
    {
        void Seed(AssignmentManagerDbContext dbContext, IServiceProvider serviceProvider);
    }
}