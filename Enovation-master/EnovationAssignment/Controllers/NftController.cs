using EnovationApp.DataAccess.Models;
using EnovationAssignment.Helpers;
using EnovationAssignment.IServices;
using EnovationAssignment.Models;
using EnovationAssignment.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnovationAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftController : ControllerBase
    {
        private readonly IUserDataService _userService;
        public NftController(IUserDataService service)
        {
            _userService = service;
        }


        /// <summary>
        /// Function that returns all Distributors in DB
        /// </summary>
        /// <returns>List of all Animals</returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Root>), StatusCodes.Status200OK)]
        [Route("MintTokens")]
        public async Task<ActionResult<List<Root>>> MintTokens(string walletId, string url)
        {
            var root = MintToken.Main(walletId, url);
            return Ok(root);
        }

        /// <summary>
        /// Function that returns all Distributors in DB
        /// </summary>
        /// <returns>List of all Animals</returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Root>), StatusCodes.Status200OK)]
        [Route("GetTokens")]
        public async Task<ActionResult<List<Root>>> GetTokens(string accountId)
        {
            
            var root = getTokens.Main(accountId);
            //var root = getTokens.Main("sEdVN9uTFZVcbJwguvYBdTBooWvoCfn");
            return Ok(root);
        }

        /// <summary>
        /// Function that returns all Distributors in DB
        /// </summary>
        /// <returns>List of all Animals</returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Root>), StatusCodes.Status200OK)]
        [Route("GetOffers")]
        public async Task<ActionResult<List<Root>>> GetOffers(string nftId)
        {
            var root = getOffers.Main(nftId);
            return Ok(root);
        }        


        /// <summary>
        /// Function that returns all Distributors in DB
        /// </summary>
        /// <returns>List of all Animals</returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Root>), StatusCodes.Status200OK)]
        [Route("AcceptSellOffer")]
        public async Task<ActionResult<List<Root>>> AcceptSell(string nftId, string offerId)
        {
            var root = AcceptSellOffer.Main(nftId, offerId);
            return Ok(root);
        }


            /// <summary>
            /// Function that returns all Distributors in DB
            /// </summary>
            /// <returns>List of all Animals</returns>
            //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            [HttpGet]
            [ProducesResponseType(typeof(List<RootObjectSellOffer>), StatusCodes.Status200OK)]
            [Route("CreateSell")]
            public async Task<ActionResult<List<RootObjectSellOffer>>> CreateSell(string walletId, string amount, string nftId)
            {
                var root = CreateSellOffer.Main(walletId, amount, nftId);
                return Ok(root);
            }

            /// <summary>
            /// Method for adding User
            /// </summary>
            /// <param name="User"></param>
            /// <returns>
            /// Ok if Animal was added succesfully
            /// BadRequest(message) if validation problems
            /// </returns>
            //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] UserRequest user)
        {
            try
            {
                await _userService.CreateAsync(user);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Function to flag User as deleted
        /// </summary>
        /// <param name="id">id of the User to delete</param>
        /// <returns>
        /// </returns>
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("SoftDeleteUser/{id}")]
        public async Task<ActionResult> SoftDeleteUser(int id)
        {
            try
            {
                await _userService.SoftDeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
