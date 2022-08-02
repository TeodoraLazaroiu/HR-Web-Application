using WebAPI.Data;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
            Employees = new EmployeesRepository(this.context);
            Teams = new TeamRepository(this.context);
            Locations = new LocationRepository(this.context);
            LeaveTypes = new LeaveTypeRepository(this.context);
        }
        public IEmployeesRepository Employees
        {
            get;
            private set;
        }
        public ITeamRepository Teams
        {
            get;
            private set;
        }
        public ILocationRepository Locations
        {
            get;
            private set;
        }
        public ILeaveTypeRepository LeaveTypes
        {
            get;
            private set;
        }
        public void Dispose()
        {
            context.Dispose();
        }
        public int Save()
        {
            return context.SaveChanges();
        }
    }
}
