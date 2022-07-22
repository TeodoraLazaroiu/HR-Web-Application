using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRAPI.Data;
using HRAPI.Models.Entities;

namespace HRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalancesController : ControllerBase
    {
        private readonly DataContext _context;

        public LeaveBalancesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LeaveBalances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveBalance>>> GetLeaveBalances()
        {
          if (_context.LeaveBalances == null)
          {
              return NotFound();
          }
            return await _context.LeaveBalances.ToListAsync();
        }

        // GET: api/LeaveBalances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveBalance>> GetLeaveBalance(int id)
        {
          if (_context.LeaveBalances == null)
          {
              return NotFound();
          }
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);

            if (leaveBalance == null)
            {
                return NotFound();
            }

            return leaveBalance;
        }

        // PUT: api/LeaveBalances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveBalance(int id, LeaveBalance leaveBalance)
        {
            if (id != leaveBalance.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveBalance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveBalanceExists(id))
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

        // POST: api/LeaveBalances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveBalance>> PostLeaveBalance(LeaveBalance leaveBalance)
        {
          if (_context.LeaveBalances == null)
          {
              return Problem("Entity set 'DataContext.LeaveBalances'  is null.");
          }
            _context.LeaveBalances.Add(leaveBalance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveBalance", new { id = leaveBalance.Id }, leaveBalance);
        }

        // DELETE: api/LeaveBalances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveBalance(int id)
        {
            if (_context.LeaveBalances == null)
            {
                return NotFound();
            }
            var leaveBalance = await _context.LeaveBalances.FindAsync(id);
            if (leaveBalance == null)
            {
                return NotFound();
            }

            _context.LeaveBalances.Remove(leaveBalance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveBalanceExists(int id)
        {
            return (_context.LeaveBalances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
