using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveHistoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public LeaveHistoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LeaveHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveHistory>>> GetLeaveHistories()
        {
          if (_context.LeaveHistories == null)
          {
              return NotFound();
          }
            return await _context.LeaveHistories.ToListAsync();
        }

        // GET: api/LeaveHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveHistory>> GetLeaveHistory(int id)
        {
          if (_context.LeaveHistories == null)
          {
              return NotFound();
          }
            var leaveHistory = await _context.LeaveHistories.FindAsync(id);

            if (leaveHistory == null)
            {
                return NotFound();
            }

            return leaveHistory;
        }

        // PUT: api/LeaveHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveHistory(int id, LeaveHistory leaveHistory)
        {
            if (id != leaveHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveHistoryExists(id))
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

        // POST: api/LeaveHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LeaveHistory>> PostLeaveHistory(LeaveHistory leaveHistory)
        {
          if (_context.LeaveHistories == null)
          {
              return Problem("Entity set 'DataContext.LeaveHistories'  is null.");
          }
            _context.LeaveHistories.Add(leaveHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveHistory", new { id = leaveHistory.Id }, leaveHistory);
        }

        // DELETE: api/LeaveHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveHistory(int id)
        {
            if (_context.LeaveHistories == null)
            {
                return NotFound();
            }
            var leaveHistory = await _context.LeaveHistories.FindAsync(id);
            if (leaveHistory == null)
            {
                return NotFound();
            }

            _context.LeaveHistories.Remove(leaveHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LeaveHistoryExists(int id)
        {
            return (_context.LeaveHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
