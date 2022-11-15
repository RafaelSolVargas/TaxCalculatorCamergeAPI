using TaxCalculator.Domain.DTOs;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services;

public class IdentityService : IIdentityService {
    private IUserRepository userRepository;

    public IdentityService(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    public User? Login(LoginRequest request) {
        var users = this.userRepository.GetAll();

        return users.FirstOrDefault(x => x.email == request.email && x.password == request.password)!;
    }
}
