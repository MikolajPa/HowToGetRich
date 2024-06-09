using EnovationApp.DataAccess.Models;

namespace EnovationAssignment.IServices
{
    public interface IAnimalService
    {
        public Task<List<AnimalDto>> GetAllAnimalListAsync();
        public Task<AnimalDto> GetAnimalByIdAsync(int id);
        public Task CreateAsync(AnimalDto Animal);
        public Task UpdateAsync(AnimalDto Animal);
        public Task HardDeleteAsync(int id);
        public Task SoftDeleteAsync(int id);
    }
}
