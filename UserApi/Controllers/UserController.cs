using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersLib;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private UserRepo _userRepo;
        public UserController(UserRepo repository)
        {
            _userRepo = repository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<User>> Get()
        {
            List<User> user = _userRepo.GetAll();

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<User>> Get(int id)
        {
            User user = _userRepo.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Post([FromBody] User user)
        {
            try
            {
                User createdUser = _userRepo.Add(user);
                return Created("/" + createdUser.Id, createdUser);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User?> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            if (int.TryParse(id, out int parsedId))
            {
                User deletedUser = _userRepo.Delete(parsedId);
                if (deletedUser == null)
                {
                    return NotFound(id);
                }
                return Ok(deletedUser);
            }
            return BadRequest();
        }
        /*[HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public ActionResult<User?> Update(int id, [FromBody] (string? name, string? password) parameters)
        public ActionResult<User?> Update(int id, [FromBody] User user)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //User updatedUser = _userRepo.Update(id, parameters.name, parameters.password);
            User updatedUser = _userRepo.Update(id, user.Name, user.Password);
            if (updatedUser == null)
            {
                return NotFound(id);
            }
            return Ok(updatedUser);
        }*/
    }
}
