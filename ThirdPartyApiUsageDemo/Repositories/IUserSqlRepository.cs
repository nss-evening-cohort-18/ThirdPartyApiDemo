using ThirdPartyApiUsageDemo.Models;

namespace ThirdPartyApiUsageDemo.Repositories
{
    public interface IUserSqlRepository
    {
        UserModel GetUser(int id);
    }
}