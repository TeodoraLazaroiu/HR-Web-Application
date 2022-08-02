namespace WebAPI.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeesRepository Employees { get; }
        ITeamRepository Teams { get; }
        ILocationRepository Locations { get; }
        ILeaveTypeRepository LeaveTypes { get; }
        int Save();
    }
}
