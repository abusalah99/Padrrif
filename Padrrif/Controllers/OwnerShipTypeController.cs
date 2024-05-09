namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerShipTypeController : ControllerBase
{
    private readonly IOwnerShipTypeUnitOfWork _unitOfWork;
    public OwnerShipTypeController(IOwnerShipTypeUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _unitOfWork.Read());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id) => Ok(await _unitOfWork.Read(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OwnerShipTypeDto dto)
    {
        OwnerShipType ownerShipType = _unitOfWork.MapFromOwnerShipTypeDtoToOwnerShipType(dto);
        await _unitOfWork.Create(ownerShipType);
        return Ok();
    }
}
