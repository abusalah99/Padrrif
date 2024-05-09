namespace Padrrif;

public interface IEducationLevelDtoUnitOfWork : IUnitOfWork<EducationLevel>
{
    EducationLevel MapFromEducationLevelDtoToEducationLevel(EducationLevelDto dto);
}