namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GovernorateController : ControllerBase
{
    private readonly IGovernorateUnitOfWork _unitOfWork;
    public GovernorateController(IGovernorateUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> Gettest() => Ok(await _unitOfWork.Read());

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _unitOfWork.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _unitOfWork.Read(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GovernorateDto dto)
    {
        Governorate governorate = _unitOfWork.MapFromGovernorateDtoToGovernorate(dto);
        await _unitOfWork.Create(governorate);
        return Ok();
    }
}
