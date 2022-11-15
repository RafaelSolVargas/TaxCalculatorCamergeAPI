using TaxCalculator.Domain.Entities;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services;

public class UserService : IUserService {
    private IUserRepository userRepository;

    public UserService(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public User Get(int id) {
        return this.userRepository.Get(id);
    }
}
