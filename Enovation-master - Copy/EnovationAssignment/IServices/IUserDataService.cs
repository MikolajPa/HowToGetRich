using EnovationApp.DataAccess.Models;
using EnovationAssignment.Models;

namespace EnovationAssignment.IServices
{
    public interface IUserDataService
    {
        public Task<List<UserDto>> GetAllDistributors();
        public Task CreateAsync(UserRequest user);
        public Task SoftDeleteAsync(int id);
    }
}
