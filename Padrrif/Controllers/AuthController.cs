using Microsoft.AspNetCore.Mvc;

namespace Padrrif.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUnitOfWork _unitOfWork;

        public AuthController(IAuthUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("test");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            TokenDto? tokenDto = await _unitOfWork.Login(request); 

            if (tokenDto != null) 
                return Ok(tokenDto);

            return BadRequest("Wrong credentials");
        }
        [HttpPost("register/farmer")]
        public async Task<IActionResult> RegisterAsFarmer([FromForm] UserRegistrationDto request)
        {
            User user = await _unitOfWork.MapFromUserRegistrationDtoToUser(request);

            TokenDto tokenDto = await _unitOfWork.RegisterAsFarmer(user);

            return Ok(tokenDto);

        }
        [HttpPost("register/empolyee")]
        public async Task<IActionResult> RegisterAsEmpolyee([FromForm] UserRegistrationDto request)
        {
            User user = await _unitOfWork.MapFromUserRegistrationDtoToUser(request);

            TokenDto tokenDto = await _unitOfWork.RegisterAsEmpolyee(user);

            return Ok(tokenDto);

        }

    }
}
