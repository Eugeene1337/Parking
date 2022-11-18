using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parking.Data;
using Parking.Models.DTO;

namespace Parking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<User>> GetAll()
        {
            return _mapper.Map<List<IdentityUser>, List<User>>(_context.Users.ToList());
        }

        [HttpGet]
        [Route("Get{id}")]
        public ActionResult<User> Get(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            var userDto = _mapper.Map<IdentityUser, User>(user);

            if (userDto == null)
            {
                return NotFound();
            }

            return userDto;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterResponse { Status = "Error", Message = $"User creation failed! Please check user details and try again." });

            return Ok(new RegisterResponse { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userDto = _mapper.Map<IdentityUser, User>(user);

                return Ok(new
                {
                    User = userDto,
                    Message = "Login success",
                });
            }
            return Unauthorized();
        }

    }
}
