namespace Padrrif.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DamageController : ControllerBase
{
    private readonly IDamageUnitOfWork _unitOfWork;

    public DamageController(IDamageUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [Authorize(Roles = nameof(RoleEnum.Farmer))]
    [HttpGet("farmer")]
    public async Task<IActionResult> GetFramerDamages() => Ok(await _unitOfWork.GetFramerDamages());

    [Authorize(Roles = nameof(RoleEnum.Empolyee))]
    [HttpGet("employee")]
    public async Task<IActionResult> GetDamageForEmpolyee() => Ok(await _unitOfWork.GetDamageForEmpolyee());

    [Authorize(Roles = nameof(RoleEnum.Empolyee))]
    [HttpGet("employee/{id}")]
    public async Task<IActionResult> GetDamageById(Guid id) => Ok(await _unitOfWork.Read(id));

    [Authorize(Roles = nameof(RoleEnum.Farmer))]
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] DamageDto dto)
    {
        Damage damage = await _unitOfWork.MapFromDamageDtoToDamage(dto);

        await _unitOfWork.Create(damage);
        return Ok();
    }

    [Authorize(Roles = nameof(RoleEnum.Empolyee))]
    [HttpPut]
    public async Task<IActionResult> Put([FromForm] DamageDto dto)
    {
        Damage damage = await _unitOfWork.MapFromDamageDtoToDamage(dto);

        await _unitOfWork.Update(damage);
        return Ok();
    }
}