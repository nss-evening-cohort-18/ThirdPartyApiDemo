using ThirdPartyApiUsageDemo.Models;

namespace ThirdPartyApiUsageDemo.Repositories
{
    public class UserSqlRepository : IUserSqlRepository
    {
        private static List<UserModel> _data = new()
        {
            new UserModel{Id = 1, Name = "Brian", FavoriteCharacterId = 1},
            new UserModel{Id = 2, Name = "Hannah", FavoriteCharacterId = 2},
        };

        public UserModel GetUser(int id)
        {
            return _data.Find(u => u.Id == id);
        }
    }
}
