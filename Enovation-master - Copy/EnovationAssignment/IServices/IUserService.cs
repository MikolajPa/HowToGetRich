using EnovationAssignment.Models;

namespace EnovationAssignment.IServices
{
    public interface IUserService
    {
        public Task<string> GetUserAsync(UserLogin userLogin);
    }
}
