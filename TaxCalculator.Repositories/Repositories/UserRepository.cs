using TaxCalculator.Domain.Entities;
using TaxCalculator.Repositories.Interfaces;

namespace TaxCalculator.Repositories;

public class UserRepository : IUserRepository {
    private Dictionary<int, User> users = new Dictionary<int, User>() {
        {1, new User(1, "user1@gmail.com", "UserPass123?")},
        {2, new User(2, "user2@gmail.com", "UserPass123?")},
        {3, new User(3, "user3@gmail.com", "UserPass123?")},
    };

    public User Get(int id) {
        return this.users.First(x => x.Key == id).Value;
    }
}
