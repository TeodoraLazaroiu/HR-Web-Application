using HRAPI.Data;
using HRAPI.Repository.Interfaces;

namespace HRAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext context;
        public UnitOfWork(DataContext context)
        {
            this.context = context;
            Employees = new EmployeesRepository(this.context);
        }
        public IEmployeesRepository Employees
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
