namespace Padrrif;
public class DamageUnitOfWork : UnitOfWork<Damage>, IDamageUnitOfWork
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IWebHostEnvironment _env;
    private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
    private readonly NotificationHubConecctedUsers _conecctedUsers;
    private readonly IRepository<Notifaction> _notifactionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Damage> _repository;

    public DamageUnitOfWork(IRepository<Damage> repository, IHttpContextAccessor contextAccessor, IWebHostEnvironment env,
        IHubContext<NotificationHub, INotificationClient> hubContext,
        IRepository<Notifaction> notifactionRepository,
        NotificationHubConecctedUsers conecctedUsers,
        IRepository<User> userRepository) : base(repository)
    {
        _contextAccessor = contextAccessor;
        _env = env;
        _hubContext = hubContext;
        _notifactionRepository = notifactionRepository;
        _userRepository = userRepository;
        _conecctedUsers = conecctedUsers;
        _repository = repository;
    }
    public async Task<List<Damage>> GetFramerDamages()
    {
        Guid farmerId = _contextAccessor.GetUserId();
        List<Damage> damages  = await Read(q => q.Where(e => e.FarmerId == farmerId)
                                                 .Include(e => e.AnimalDamages)
                                                 .Include(e => e.WorkHours)
                                                 .Include(e => e.PlantDamages)
                                                 .Include(e => e.FisheryDamages)
                                                 .Include(e => e.FarmFacilities)
                                                 .Include(e => e.Employee)
                                                 .ThenInclude(e => e.Comittee));
        foreach(var damage in damages)
            damage.DocumentNumber = $"{damage.CreatedAt.Date} {damage.DocumentId}";

        return damages;
    }
    public async override Task<Damage?> Read(Guid id, Func<IQueryable<Damage>, IQueryable<Damage>>? additionalQuery = null)
    {
        Damage? damage = await base.Read(id, q => q.Include(e => e.AnimalDamages)
                                                   .Include(e => e.WorkHours)
                                                   .Include(e => e.PlantDamages)
                                                   .Include(e => e.FisheryDamages)
                                                   .Include(e => e.FarmFacilities)
                                                   .Include(e => e.Employee)
                                                   .ThenInclude(e => e.Comittee));
        if (damage != null)
            damage.DocumentNumber = $"{damage.CreatedAt.Date} {damage.DocumentId}";

        return damage;
    }
    public async Task<List<Damage>> GetDamageForEmpolyee()
    {
        Guid employeeId = _contextAccessor.GetUserId();
        User? employee = await _userRepository.GetById(employeeId);

        if (employee != null)
        {
            List<Guid> farmersId = await _userRepository.SelectListOfProperty(e => e.Id, q => q.Where(e => e.GovernorateId == employee.GovernorateId));
            List<Damage> damages = await Read(q => q.Where(e => e.EmployeeId == null && farmersId.Contains(e.FarmerId))
                                                    .Include(e => e.AnimalDamages)
                                                    .Include(e => e.WorkHours)
                                                    .Include(e => e.PlantDamages)
                                                    .Include(e => e.FisheryDamages)
                                                    .Include(e => e.FarmFacilities)
                                                    .Include(e => e.Employee)
                                                    .ThenInclude(e => e.Comittee));
            return damages;
        }
        return new();
    }
    public async override Task Update(Damage entity)
    {
        entity.EmployeeId = _contextAccessor.GetUserId();
        await _repository.HardUpdateEntity(entity);
    }

    public async override Task Create(Damage entity)
    {
        Guid userId = _contextAccessor.GetUserId();

        User user = await _userRepository.GetById(userId) ?? new();

        List<User> employees = await _userRepository.GetList(q => q.Where(e => e.Role == RoleEnum.Empolyee && e.GovernorateId == user.GovernorateId));

        List<HubConnectedUser> onlineUsers = _conecctedUsers.HubConnectedUsers.Where(e => e.Role == RoleEnum.Empolyee && e.GovernorateId == user.GovernorateId)
                                                                              .ToList();

        List<Guid> onlineUsersIds = onlineUsers.Select(e => e.Id)
                                               .ToList();

        List<Guid> offlineEmployeesIds = employees.Where(e => !onlineUsersIds.Contains(e.Id))
                                                  .Select(e => e.Id)
                                                  .ToList();

        List<string> userHubIds = onlineUsers.Select(e => e.ConnectionId).ToList();


        string notifactionMessage = $"تم انشاء ضرر للمزراع {user.Name}";

        foreach (var id in userHubIds)
            await _hubContext.Clients.Client(id).ReciveNotification(notifactionMessage);

        foreach (var id in offlineEmployeesIds)
        {
            Notifaction notifaction = new()
            {
                Message = notifactionMessage,
                UserId = id,
                SeenAt = null
            };
            await _notifactionRepository.Add(notifaction);

        }
        entity.FarmerId = userId;
        entity.Id = Guid.Empty;

        await base.Create(entity);
    }
    public async Task<Damage> MapFromDamageDtoToDamage(DamageDto dto)
    {
        Damage damage = new()
        {
            Id = dto.Id,
            VillageId = dto.VillageId,
            TotalArea = dto.TotalArea,
            StartDate = dto.StartDate,
            otherTabThreeText = dto.otherTabThreeText,
            ChildrenUnderEighteen = dto.ChildrenUnderEighteen,
            BasinNumber = dto.BasinNumber,
            CompanyOrFactoryName = dto.CompanyOrFactoryName,
            ContractDuration = dto.ContractDuration,
            ContractorId = dto.ContractorId,
            ContractorName = dto.ContractorName,
            DamageRateLastFiveYears = dto.DamageRateLastFiveYears,
            EventDate = dto.EventDate,
            EventDescription = dto.EventDescription,
            EventDuration = dto.EventDuration,
            IsDroughtChecked = dto.IsDroughtChecked,
            IsExtremeTemperatureDropChecked = dto.IsExtremeTemperatureDropChecked,
            EducationLevelId = dto.EducationLevelId,
            FamilyMembers = dto.FamilyMembers,
            IsAnimalTremblingChecked = dto.IsAnimalTremblingChecked,
            ReliancePercentage = dto.ReliancePercentage,
            IsBirdFluChecked = dto.IsBirdFluChecked,
            SettlementName = dto.SettlementName,
            RegionType = dto.RegionType,
            OwnershipTypeId = dto.OwnershipTypeId,
            OtherTabTwoText = dto.OtherTabTwoText,
            MonthlyIncomeFromAgriculture = dto.MonthlyIncomeFromAgriculture,
            Longitude = dto.Longitude,
            Latitude = dto.Latitude,
            LandNumber = dto.LandNumber,
            IsStrongWindChecked = dto.IsStrongWindChecked,
            IsSnowChecked = dto.IsSnowChecked,
            IsOtherProfession = dto.IsOtherProfession,
            IsSeparationWallChecked = dto.IsSeparationWallChecked,
            IsMilitaryZoneChecked = dto.IsMilitaryZoneChecked,
            IsExtremeTemperatureRiseChecked = dto.IsExtremeTemperatureRiseChecked,
            IsFloodChecked = dto.IsFloodChecked,
            IsFrostChecked = dto.IsFrostChecked,
            IsHailChecked = dto.IsHailChecked,
            IsHarmfulWeedsChecked = dto.IsHarmfulWeedsChecked,
            IsIsraeliArmyChecked = dto.IsIsraeliArmyChecked,
            IsLocustChecked = dto.IsLocustChecked,
            IsMarketClosedChecked = dto.IsMarketClosedChecked
        }; 
        if(dto.AnimalDamages != null)
        {
            List<AnimalDamage> animalDamages = new();

            foreach(var animalDamage in dto.AnimalDamages)
                animalDamages.Add(MapFromAnimalDamageDtoToAnimalDamage(animalDamage));

            damage.AnimalDamages = animalDamages;
        }
        if (dto.WorkHours != null)
        {
            List<WorkHours> workHours = new();

            foreach (var workHour in dto.WorkHours)
                workHours.Add(MapFromWorkHoursDtoToWorkHours(workHour));

            damage.WorkHours = workHours;
        }

        if (dto.FarmFacilities != null)
        {
            List<FarmFacilities> farmFacilities = new();

            foreach (var farmFacility in dto.FarmFacilities)
                farmFacilities.Add(MapFromFarmFacilitiesDtoToFarmFacilities(farmFacility));

            damage.FarmFacilities = farmFacilities;
        }
        if (dto.PlantDamages != null)
        {
            List<PlantDamage> plantDamages = new();

            foreach (var plantDamage in dto.PlantDamages)
                plantDamages.Add(MapFromPlantDamageDtoToPlantDamage(plantDamage));

            damage.PlantDamages = plantDamages;
        }
        if (dto.FisheryDamages != null)
        {
            List<FisheryDamage> fisheryDamages = new();

            foreach (var fisheryDamage in dto.FisheryDamages)
                fisheryDamages.Add(MapFromFisheryDamageDtoToFisheryDamage(fisheryDamage));

            damage.FisheryDamages = fisheryDamages;
        }

        List<string>? documentsPaths = null;
        if (dto.Documents != null && dto.Documents.Any())
        {
            documentsPaths = new();
            foreach (var document in dto.Documents)
            {
                string path = await document.SaveImageAsync(_env);
                documentsPaths.Add(path.GetFileUrl(_contextAccessor) ?? "");
            }
            damage.DocumentsPaths = documentsPaths;
        }
        List<string>? LocationIamgesPaths = null;
        if (dto.LocationImages != null && dto.LocationImages.Any())
        {
            LocationIamgesPaths = new();
            foreach (var locationImage in dto.LocationImages)
            {
                string path = await locationImage.SaveImageAsync(_env);
                LocationIamgesPaths.Add(path.GetFileUrl(_contextAccessor) ?? "");
            }
            damage.LocationPaths = LocationIamgesPaths;
        }

        return damage;
    }
    public AnimalDamage MapFromAnimalDamageDtoToAnimalDamage(AnimalDamageDto dto)
        => new()
        {
            DamageType = dto.DamageType,
            PriceBeforeDamage = dto.PriceBeforeDamage,
            AffectedNumber = dto.AffectedNumber,
            Insurance = dto.Insurance,
            InsuranceProvider = dto.InsuranceProvider,
            ProductionRate = dto.ProductionRate,
            TotalNumber = dto.TotalNumber,
            Type = dto.Type,
        };
    public FarmFacilities MapFromFarmFacilitiesDtoToFarmFacilities(FarmFacilitiesDto dto)
        => new()
        {
            ActualDamageArea = dto.ActualDamageArea,
            CategoryFacilities = dto.CategoryFacilities,
            DamageFacilitiesType = dto.DamageFacilitiesType,
            FacilitiesType = dto.FacilitiesType,
            InsuranceFacilities = dto.InsuranceFacilities,
            InsuranceProvider = dto.InsuranceProvider,
            LicenseFacilities = dto.LicenseFacilities,
            LicenseProvider = dto.LicenseProvider,
            TotalAffectedArea = dto.TotalAffectedArea
        };
    public WorkHours MapFromWorkHoursDtoToWorkHours(WorkHoursDto dto)
        => new()
        {
            DailyWorkHours = dto.DailyWorkHours,
            WeeklyWorkHours = dto.WeeklyWorkHours,
            Gender = dto.Gender,
            Name = dto.Name,
        };
    public PlantDamage MapFromPlantDamageDtoToPlantDamage(PlantDamageDto dto)
        => new()
        {
            ActualDamageArea = dto.ActualDamageArea,
            CropAge = dto.CropAge,
            CropDamage = dto.CropDamage,
            CropType = dto.CropType,
            CropTypeMethod = dto.CropTypeMethod,
            CropTypeOption = dto.CropTypeOption,
            EstimatedPrice = dto.EstimatedPrice,
            EstimatedProductionRate = dto.EstimatedProductionRate,
            FruitingStage = dto.FruitingStage,
            Insurance = dto.Insurance,
            InsuranceProvider = dto.InsuranceProvider,
            IrrigationMethod = dto.IrrigationMethod,
            IrrigationOption = dto.IrrigationOption,
            OtherCropTextFieldValue = dto.OtherCropTextFieldValue,
            OtherTextFieldValue = dto.OtherTextFieldValue,
            PercentageDamage = dto.PercentageDamage,
            TotalAffectedArea = dto.TotalAffectedArea
        };
    public FisheryDamage MapFromFisheryDamageDtoToFisheryDamage(FisheryDamageDto dto)
        => new()
        {
            AffectedNumber = dto.AffectedNumber,
            Category = dto.Category,
            FishDamageType = dto.FishDamageType,
            Insurance = dto.Insurance,
            InsuranceProvider = dto.InsuranceProvider,
            PriceBeforeDamage = dto.PriceBeforeDamage,
            ProductionRate = dto.ProductionRate,
            TotalNumber = dto.TotalNumber,
            Type = dto.Type,
        };
}