namespace Padrrif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public UserController(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [Authorize(Roles = nameof(RoleEnum.Empolyee))]
        [HttpGet("unconfirmed-users")]
        public async Task<IActionResult> Get()
        {
            List<User> users = await _unitOfWork.GetAllUnConfirmedUsers();

            return Ok(users);   
        }
        [Authorize(Roles = nameof(RoleEnum.Empolyee))]
        [HttpPost("confirm-user")]
        public async Task<IActionResult> ConfirmUser(Guid id)
        {
            bool isUpdated = await _unitOfWork.ConfirmUser(id);

            if (isUpdated)
                return Ok();

            return BadRequest();
        }
    }
}
