using WebAPI.Models.Entities;

namespace WebAPI.Models.DTOs
{
    public class LeaveBalanceDTO
    {
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public int DaysTotal { get; set; } = 20;
        public int DaysRemaining { get; set; } = 20;
        public int DaysTaken { get; set; } = 0;

        public LeaveBalanceDTO(string firstName, string lastName, LeaveBalance leaveBalance)
        {
            this.EmployeeFirstName = firstName;
            this.EmployeeLastName = lastName;
            this.DaysTotal = leaveBalance.DaysTotal;
            this.DaysRemaining = leaveBalance.DaysRemaining;
            this.DaysTaken = leaveBalance.DaysTaken;
        }
    }
}
