using GlobalExceptionHandling.Data;
using GlobalExceptionHandling.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalExceptionHandling.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly ApplicationDbContext _context;

        public DriverServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Driver> AddDriver(Driver driver)
        {
            var result = await _context.drivers.AddAsync(driver);

            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteDriver(int id)
        {
            var driverToDelete = await GetDriverById(id);
            if (driverToDelete == null)
            {
                return false;
            }

            _context.Remove(driverToDelete);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Driver?> GetDriverById(int id)
        {
            var result = await _context.drivers.FirstOrDefaultAsync(d => d.Id == id);
            return result;
        }

        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            return await _context.drivers.ToListAsync();
        }

        public async Task<Driver> UpdateDriver(Driver driver)
        {
            var result = _context.drivers.Update(driver);
            await _context.SaveChangesAsync();
            return result.Entity;

        }
    }
}
