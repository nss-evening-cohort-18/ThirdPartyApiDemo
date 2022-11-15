namespace ThirdPartyApiUsageDemo.Models;

public class ProfileViewModel
{
    public UserModel User { get; set; }
    public SwapiPerson FavoriteStarWarsCharacter { get; set; }
    public List<SwapiPersonOptions> PersonOptions { get; set; }
}
