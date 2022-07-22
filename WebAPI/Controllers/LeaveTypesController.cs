using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRAPI.Data;
using HRAPI.Models.Entities;

namespace HRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public LeaveTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveType>>> GetLeaveTypes()
        {
          if (_context.LeaveTypes == null)
          {
              return NotFound();
          }
            return await _context.LeaveTypes.ToListAsync();
        }

        // GET: api/LeaveTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveType>> GetLeaveType(int id)
        {
          if (_context.LeaveTypes == null)
          {
              return NotFound();
          }
            var leaveType = await _context.LeaveTypes.FindAsync(id);

            if (leaveType == null)
            {
                return NotFound();
            }

            return leaveType;
        }

        // PUT: api/LeaveTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveType(int id, LeaveType leaveType)
        {
            if (id != leaveType.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveType>> PostLeaveType(LeaveType leaveType)
        {
          if (_context.LeaveTypes == null)
          {
              return Problem("Entity set 'DataContext.LeaveTypes'  is null.");
          }
            _context.LeaveTypes.Add(leaveType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveType", new { id = leaveType.Id }, leaveType);
        }

        // DELETE: api/LeaveTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveType(int id)
        {
            if (_context.LeaveTypes == null)
            {
                return NotFound();
            }
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveTypeExists(int id)
        {
            return (_context.LeaveTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
