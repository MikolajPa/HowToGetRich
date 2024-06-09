using EnovationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.IRepositories
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUserAsync(UserLogin userLogin);
    }
}
