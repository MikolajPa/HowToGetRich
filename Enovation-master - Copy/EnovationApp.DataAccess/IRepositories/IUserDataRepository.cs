using EnovationApp.DataAccess.Models;
using EnovationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.IRepositories
{
    public interface IUserDataRepository
    {
        public Task<List<UserDto>> GetAllDistributors();

        public Task SoftDeleteAsync(int id);

        public Task CreateAsync(UserRequest user);
    }
}
