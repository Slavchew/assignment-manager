namespace AssignmentManager.Services
{
    public interface IAssignmentsService
    {
        void Create(string name, string className, string dueDate, string desription);

        void Edit(string name, string className, string dueDate, string desription);
    }
}
