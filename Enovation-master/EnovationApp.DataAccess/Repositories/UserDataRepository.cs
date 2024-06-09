using AutoMapper;
using EnovationApp.DataAccess.IRepositories;
using EnovationApp.DataAccess.Models;
using EnovationAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserDataRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(UserRequest user)
        {
            _context.Users.Add(_mapper.Map<User>(user));
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllDistributors()
        {
            return _mapper.Map<List<UserDto>>(_context.Users.Where(x => x.IsDistributor == true && x.IsDeleted == false).ToList());
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return _mapper.Map<UserDto>(_context.Users.FirstOrDefault(x => x.Id == id));
        }

        //public async Task HardDeleteAsync(int id)
        //{
        //    Animal Animal = new Animal() { Id = id };
        //    _context.Animals.Attach(Animal);
        //    _context.Animals.Remove(Animal);
        //    await _context.SaveChangesAsync();
        //}

        public async Task SoftDeleteAsync(int id)
        {
            _context.Users.First(x => x.Id == id).IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalDto Animal)
        {
            _context.Update(_mapper.Map<Animal>(Animal));
            await _context.SaveChangesAsync();
        }
    }
}
