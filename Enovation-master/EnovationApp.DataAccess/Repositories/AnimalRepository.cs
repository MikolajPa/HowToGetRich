using AutoMapper;
using EnovationApp.DataAccess.IRepositories;
using EnovationApp.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AnimalRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateAsync(AnimalDto Animal)
        {
            _context.Animals.Add(_mapper.Map<Animal>(Animal));
            await _context.SaveChangesAsync();
        }

        public async Task<List<AnimalDto>> GetAllAnimalListAsync()
        {
            return _mapper.Map<List<AnimalDto>>(_context.Animals.Where(x=>x.IsDeleted==false).ToList());
        }

        public async Task<AnimalDto> GetAnimalByIdAsync(int id)
        {
            return _mapper.Map<AnimalDto>(_context.Animals.FirstOrDefault(x=>x.Id==id));
        }

        public async Task HardDeleteAsync(int id)
        {
            Animal Animal = new Animal() { Id = id };
            _context.Animals.Attach(Animal);
            _context.Animals.Remove(Animal);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            _context.Animals.First(x=>x.Id==id).IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalDto Animal)
        {
            _context.Update(_mapper.Map<Animal>(Animal));
            await _context.SaveChangesAsync();
        }
    }
}
