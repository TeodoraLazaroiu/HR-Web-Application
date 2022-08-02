using WebAPI.Models.Entities;

namespace WebAPI.Models.DTOs
{
    public class LeaveTypeDTO
    {
        public string LeaveName { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        public string Description { get; set; } = string.Empty;

        public LeaveTypeDTO(LeaveType leaveType)
        {
            this.LeaveName = leaveType.LeaveName;
            this.NumberOfDays = leaveType.NumberOfDays;
            this.Description = leaveType.Description;
        }
    }
}
