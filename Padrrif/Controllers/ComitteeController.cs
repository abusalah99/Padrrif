namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComitteeController : ControllerBase
{
    private readonly IComitteeUnitOfWork _unitOfWork;
    public ComitteeController(IComitteeUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _unitOfWork.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _unitOfWork.Read(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComiteeDto dto)
    {
        Comitee comitee = _unitOfWork.MapFromComitteeDtoToComittee(dto);
        await _unitOfWork.Create(comitee);
        return Ok();
    }
}
