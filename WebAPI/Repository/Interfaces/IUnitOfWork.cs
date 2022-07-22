namespace HRAPI.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeesRepository Employees { get; }
        int Save();
    }
}
