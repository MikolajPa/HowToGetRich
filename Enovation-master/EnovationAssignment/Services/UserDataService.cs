using EnovationApp.DataAccess.IRepositories;
using EnovationApp.DataAccess.Migrations;
using EnovationApp.DataAccess.Models;
using EnovationApp.DataAccess.Repositories;
using EnovationAssignment.Helpers;
using EnovationAssignment.IServices;
using EnovationAssignment.Models;
using EnovationAssignment.Validators;
using FluentValidation;

namespace EnovationAssignment.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IValidator<UserRequest> _userValidator;
        public UserDataService(IUserDataRepository userDataRepository, IValidator<UserRequest> userValidator)
        {
            _userDataRepository = userDataRepository;
            _userValidator = userValidator;
        }

        public async Task CreateAsync(UserRequest user)
        {
            var validationResult = _userValidator.Validate(user);
            if (!validationResult.IsValid)
                throw new AppException(validationResult.ToString());
            await _userDataRepository.CreateAsync(user);
        }

        public async Task<List<UserDto>> GetAllDistributors()
        {
            return await _userDataRepository.GetAllDistributors();
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _userDataRepository.SoftDeleteAsync(id);
        }

        //public async Task SoftDeleteAsync(int id)
        //{
        //    await _AnimalRepository.SoftDeleteAsync(id);
        //}

    }
}
