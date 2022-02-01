using AssignmentManager.Data;

namespace AssignmentManager.Services.Implementations
{
    public class AssignmentsService : IAssignmentsService
    {
        private AssignmentManagerDbContext db;

        public AssignmentsService(AssignmentManagerDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string className, string dueDate, string desription)
        {
            // Todo:
        }

        public void Edit(string name, string className, string dueDate, string desription)
        {
            // Todo:
        }
    }
}
