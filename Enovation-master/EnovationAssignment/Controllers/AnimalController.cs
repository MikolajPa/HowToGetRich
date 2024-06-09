//using EnovationApp.DataAccess.Models;
//using EnovationAssignment.Helpers;
//using EnovationAssignment.IServices;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace EnovationAssignment.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AnimalController : ControllerBase
//    {
//        private readonly IAnimalService _AnimalService;
//        public AnimalController(IAnimalService service)
//        {
//            _AnimalService = service;
//        }


//        /// <summary>
//        /// Function to flag Animal as deleted
//        /// </summary>
//        /// <param name="id">id of the Animal to delete</param>
//        /// <returns>
//        /// </returns>
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpPut]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [Route("SoftDeleteAnimal/{id}")]
//        public async Task<ActionResult> SoftDeleteAnimal(int id)
//        {
//            try
//            {
//                await _AnimalService.SoftDeleteAsync(id);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        /// <summary>
//        /// Function to remove Animal from DB
//        /// </summary>
//        /// <param name="id">id of the Animal to remove</param>
//        /// <returns>
//        /// </returns>
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpDelete]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [Route("HardDeleteAnimal/{id}")]
//        public async Task<ActionResult> HardDeleteAnimal(int id)
//        {
//            try
//            {
//                await _AnimalService.HardDeleteAsync(id);
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        /// <summary>
//        /// Method for adding Animal
//        /// </summary>
//        /// <param name="Animal"></param>
//        /// <returns>
//        /// Ok if Animal was added succesfully
//        /// BadRequest(message) if validation problems
//        /// </returns>
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [Route("AddAnimal")]
//        public async Task<ActionResult> AddAnimal([FromBody]AnimalDto Animal)
//        {
//            try
//            {
//                await _AnimalService.CreateAsync(Animal);
//                return Ok();
//            }
//            catch(AppException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//        /// <summary>
//        /// Function that returns all Animals in DB
//        /// </summary>
//        /// <returns>List of all Animals</returns>
//        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpGet]
//        [ProducesResponseType(typeof(List<AnimalDto>),StatusCodes.Status200OK)]
//        [Route("GetAllAnimals")]
//        public async Task<ActionResult<List<AnimalDto>>> GetAllAnimals()
//        {
//            return Ok(await _AnimalService.GetAllAnimalListAsync());
//        }
//        /// <summary>
//        /// Function that returns Animal with given id
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns>
//        /// 200 and Animal object,
//        /// 404, if Animal was not found
//        /// </returns>
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpGet]
//        [ProducesResponseType(typeof(AnimalDto), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Exception), StatusCodes.Status404NotFound)]
//        [Route("GetAnimalById/{id}")]
//        public async Task<ActionResult> GetAnimalById(int id)
//        {
//            try
//            {
//                return Ok(await _AnimalService.GetAnimalByIdAsync(id));
//            }
//            catch(Exception ex)
//            {
//                return NotFound(ex.Message);
//            }
//        }
//        /// <summary>
//        /// Function that updates Animal
//        /// </summary>
//        /// <param name="Animal"></param>
//        /// <returns>
//        /// 200 if ok
//        /// 400 if validation problem
//        /// </returns>
//        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//        [HttpPut]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        [Route("UpdateAnimal")]
//        public async Task<ActionResult> UpdateAnimal([FromBody]AnimalDto Animal)
//        {
//            try
//            {
//                await _AnimalService.UpdateAsync(Animal);
//                return Ok();
//            }
//            catch (AppException ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//    }
//}
