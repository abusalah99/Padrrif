namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationLevelController : ControllerBase
{
    private readonly IEducationLevelDtoUnitOfWork _unitOfWork;
    public EducationLevelController(IEducationLevelDtoUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _unitOfWork.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _unitOfWork.Read(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EducationLevelDto dto)
    {
        EducationLevel educationLevel = _unitOfWork.MapFromEducationLevelDtoToEducationLevel(dto);
        await _unitOfWork.Create(educationLevel);
        return Ok();
    }
}