using AssignmentManager.Data;

namespace AssignmentManager.Services.Implementations
{
    public class ClassesService : IClassesService
    {
        private AssignmentManagerDbContext db;

        public ClassesService(AssignmentManagerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string color)
        {
            // Todo:
        }

        public void Edit(string name, string color)
        {
            // Todo:
        }
    }
}
