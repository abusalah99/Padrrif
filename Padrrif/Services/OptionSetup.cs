namespace Padrrif;

public class OptionSetup<TEntity> : IConfigureOptions<TEntity> where  TEntity : class
{
    public string SectionName { get; set; } 

    private readonly IConfiguration _configuration;

    public OptionSetup(IConfiguration configuration, string sectionName)
    {
        _configuration = configuration;
        SectionName = sectionName;
    }

    public void Configure(TEntity options)
           => _configuration.GetSection(SectionName).Bind(options);

}
