//using EnovationApp.DataAccess.IRepositories;
//using EnovationApp.DataAccess.Models;
//using EnovationApp.Domain.Enums;
//using EnovationAssignment.Helpers;
//using EnovationAssignment.Services;
//using EnovationAssignment.Validators;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace EnovationAssignment.Tests
//{
    
//    public class AnimalServiceTest
//    {
//        private Mock<IAnimalRepository> mockAnimalRepository = new Mock<IAnimalRepository>();
//        [Fact]
//        public void AddAnimalToDataBaseHappyPathTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = 0,
//                Type = AnimalEnum.Dog
//            };

//            mockAnimalRepository.Setup(r => r.CreateAsync(Animal)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object,  new AnimalValidator());

//            service.CreateAsync(Animal).Wait();

//            mockAnimalRepository.Verify(r => r.CreateAsync(Animal), Times.Once);
//        }
//        [Fact]
//        public void AddAnimalToDataBaseUnhappyPathTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = -3,
//                Type = AnimalEnum.None
//            };
//            mockAnimalRepository.Setup(r => r.CreateAsync(Animal)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());
//            Assert.ThrowsAsync<AppException>(async () => await service.CreateAsync(Animal));
            
//        }
//        [Fact]
//        public void GetAllAnimalsFromDBTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = 0,
//                Type = AnimalEnum.Dog
//            };
//            List<AnimalDto> listOfAnimals = new List<AnimalDto>();
//            listOfAnimals.Add(Animal);


//            mockAnimalRepository.Setup(r => r.GetAllAnimalListAsync()).Returns(Task.FromResult(listOfAnimals));

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());

//            service.GetAllAnimalListAsync().Wait();

//            mockAnimalRepository.Verify(r => r.GetAllAnimalListAsync(), Times.Once);
//        }
//        [Fact]
//        public void UpdateAnimalInDataBaseHappyPathTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = 0,
//                Type = AnimalEnum.Dog
//            };

//            mockAnimalRepository.Setup(r => r.UpdateAsync(Animal)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());

//            service.UpdateAsync(Animal).Wait();

//            mockAnimalRepository.Verify(r => r.UpdateAsync(Animal), Times.Once);
//        }
//        [Fact]
//        public void UpdateAnimalInDataBaseUnhappyPathTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = -3,
//                Type = AnimalEnum.None
//            };
//            mockAnimalRepository.Setup(r => r.UpdateAsync(Animal)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());
//            Assert.ThrowsAsync<AppException>(async () => await service.UpdateAsync(Animal));

//        }
//        [Fact]
//        public void GetAnimalFromDBTest()
//        {
//            AnimalDto Animal = new()
//            {
//                Name = "aaaa",
//                Id = 0,
//                Age = 20,
//                Legs = 0,
//                Type = AnimalEnum.Dog
//            };
//            int id = 4;


//            mockAnimalRepository.Setup(r => r.GetAnimalByIdAsync(id)).Returns(Task.FromResult(Animal));

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());

//            service.GetAnimalByIdAsync(id).Wait();

//            mockAnimalRepository.Verify(r => r.GetAnimalByIdAsync(id), Times.Once);
//        }

//        [Fact]
//        public void SoftDeleteAnimalInDBTest()
//        {
//            int id = 4;
//            mockAnimalRepository.Setup(r => r.SoftDeleteAsync(id)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());

//            service.SoftDeleteAsync(id).Wait();

//            mockAnimalRepository.Verify(r => r.SoftDeleteAsync(id), Times.Once);
//        }
//        [Fact]
//        public void HardDeleteAnimalInDBTest()
//        {
//            int id = 4;
//            mockAnimalRepository.Setup(r => r.HardDeleteAsync(id)).Returns(Task.CompletedTask);

//            var service = new AnimalService(mockAnimalRepository.Object, new AnimalValidator());

//            service.HardDeleteAsync(id).Wait();

//            mockAnimalRepository.Verify(r => r.HardDeleteAsync(id), Times.Once);
//        }


//    }
//}
