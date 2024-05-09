namespace Padrrif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifactionController : ControllerBase
    {
        private readonly INotifactionUnitOfWork _unitOfWork;
        public NotifactionController(INotifactionUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _unitOfWork.GetUnSeenNotifactions());
    }
}
