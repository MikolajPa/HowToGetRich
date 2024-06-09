using EnovationApp.DataAccess.IRepositories;
using EnovationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnovationApp.DataAccess.Models;
using AutoMapper;

namespace EnovationApp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<UserDto> GetUserAsync(UserLogin userLogin)
        {
            User user = _context.Users.FirstOrDefault(
                x => x.Email == userLogin.Email
                && x.Password == userLogin.Password);
            return _mapper.Map<UserDto>(user);
        }
    }
}
