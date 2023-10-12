using GlobalExceptionHandling.Models;

namespace GlobalExceptionHandling.Services
{
    public interface IDriverServices
    {
        public Task <IEnumerable<Driver>> GetDrivers();
        public Task<Driver?> GetDriverById(int id);

        public Task<Driver> UpdateDriver(Driver driver);
        public Task<Driver> AddDriver(Driver driver);
        public Task<bool> DeleteDriver(int id);
    }
}
