namespace AssignmentManager.Services
{
    public interface IClassesService
    {
        void Create(string name, string color);

        void Edit(string name, string color);
    }
}
