using EnovationApp.DataAccess.IRepositories;
using EnovationApp.DataAccess.Models;
using EnovationAssignment.Helpers;
using EnovationAssignment.IServices;
using FluentValidation;

namespace EnovationAssignment.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _AnimalRepository;
        private readonly IValidator<AnimalDto> _AnimalValidator;
        public AnimalService(IAnimalRepository AnimalRepository, IValidator<AnimalDto> AnimalValidator)
        {
            _AnimalRepository = AnimalRepository;
            _AnimalValidator = AnimalValidator ?? throw new ArgumentException();
        }
        public async Task CreateAsync(AnimalDto Animal)
        {
            var validationResult = _AnimalValidator.Validate(Animal);
            if (!validationResult.IsValid)
                throw new AppException(validationResult.ToString());
            await _AnimalRepository.CreateAsync(Animal);
        }

        public async Task<List<AnimalDto>> GetAllAnimalListAsync()
        {
            return await _AnimalRepository.GetAllAnimalListAsync();
        }

        public async Task<AnimalDto> GetAnimalByIdAsync(int id)
        {
            return await _AnimalRepository.GetAnimalByIdAsync(id);
        }

        public async Task HardDeleteAsync(int id)
        {
            await _AnimalRepository.HardDeleteAsync(id);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _AnimalRepository.SoftDeleteAsync(id);
        }

        public async Task UpdateAsync(AnimalDto Animal)
        {
            var validationResult = _AnimalValidator.Validate(Animal);
            if (!validationResult.IsValid)
                throw new AppException(validationResult.ToString());
            await _AnimalRepository.UpdateAsync(Animal);
        }
    }
}
