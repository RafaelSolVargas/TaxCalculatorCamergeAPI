using TaxCalculator.Domain.Entities;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories;

public class UserRepository : IUserRepository {
    private Dictionary<int, User> users = new Dictionary<int, User>() {
        {1, new User(1, "user1@gmail.com", "UserPass123?", new Role(true))},
        {2, new User(2, "user2@gmail.com", "UserPass123?", new Role(true))},
        {3, new User(3, "user3@gmail.com", "UserPass123?", new Role(false))},
    };

    public User Get(int id) {
        return this.users.First(x => x.Key == id).Value;
    }

    public List<User> GetAll() {
        return this.users.Values.ToList();
    }
}
