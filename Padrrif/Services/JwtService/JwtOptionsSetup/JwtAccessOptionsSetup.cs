namespace Padrrif;

public class JwtAccessOptionsSetup : OptionSetup<JwtAccessOptions>
{   
    public JwtAccessOptionsSetup(IConfiguration configuration, string sectionName = "JwtAccess") 
        : base(configuration, sectionName) {  }

}
