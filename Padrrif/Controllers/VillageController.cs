namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VillageController : ControllerBase
{
    private readonly IVillageUnitOfWork _unitOfWork;
    public VillageController(IVillageUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _unitOfWork.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _unitOfWork.Read(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VillageDto dto)
    {
        Village village = _unitOfWork.MapFromVillageDtoToVillage(dto);
        await _unitOfWork.Create(village);
        return Ok();
    }
}
