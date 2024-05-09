namespace Padrrif.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUnitOfWork _unitOfWork;

        public AuthController(IAuthUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            TokenDto? tokenDto = await _unitOfWork.Login(request); 

            if (tokenDto != null) 
                return Ok(tokenDto);

            return BadRequest("Wrong credentials");
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsFarmer([FromForm] UserRegistrationDto request)
        {
            User user = await _unitOfWork.MapFromUserRegistrationDtoToUser(request);

            TokenDto tokenDto = await _unitOfWork.RegisterAsFarmer(user);

            return Ok(tokenDto);

        }

        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpPost("create-employee")]
        public async Task<IActionResult> CreateEmployee([FromForm] UserRegistrationDto request)
        {
            User user = await _unitOfWork.MapFromUserRegistrationDtoToUser(request);

            try
            {
                await _unitOfWork.RegisterAsEmpolyee(user);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}