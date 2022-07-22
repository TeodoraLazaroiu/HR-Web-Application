using HRAPI.Data;
using HRAPI.Models.Entities;
using HRAPI.Repository.Interfaces;

namespace HRAPI.Repository
{
    public class EmployeesRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(DataContext context) : base(context) { }
    }
}
