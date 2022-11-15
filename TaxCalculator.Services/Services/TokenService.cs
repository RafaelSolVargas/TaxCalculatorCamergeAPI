using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaxCalculator.Domain.Configurations;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Exceptions;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services;

public class TokenService : ITokenService {
    private IIdentityService identityService;
    private IUserService userService;
    private string userIDKey = "userIDKey";
    private JwtOptions jwtOptions;

    public TokenService(IIdentityService identityService, IUserService userService) {
        this.identityService = identityService;
        this.userService = userService;
        this.jwtOptions = Settings.GetInstance().jwtOptions!;
    }

    public string GenerateAccessToken(User user) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = Encoding.ASCII.GetBytes(this.jwtOptions.JwtSecretKey!);

        // Create an claimIdentity object to be converted to JWT
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim($"{this.userIDKey}", user.id!.ToString()!));

        // Cria as credenciais para geração do JWT
        var key = new SymmetricSecurityKey(jwtKey);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // Cria o descriptor do Token
        var tokenDescriptor = new SecurityTokenDescriptor();
        tokenDescriptor.Issuer = this.jwtOptions.Issuer;
        tokenDescriptor.Audience = this.jwtOptions.Audience;
        tokenDescriptor.Subject = claims;
        tokenDescriptor.Expires = DateTime.UtcNow.AddHours((this.jwtOptions.AccessTokenExpirationHours));
        tokenDescriptor.SigningCredentials = credentials;

        // Converte para Token e retorna
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public User ValidateAccessToken(string token) {
        if (token == null)
            return null!;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.JwtSecretKey!);
        var validationParams = new TokenValidationParameters() {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidAudience = jwtOptions.Audience,
            ValidIssuer = jwtOptions.Issuer
        };

        try {
            tokenHandler.ValidateToken(token, validationParams, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userID = jwtToken.Claims.First(x => x.Type == this.userIDKey).Value;
            var userIDInt = Int32.Parse(userID);

            return this.userService.Get(userIDInt);
        } catch (Exception error) {
            throw new InvalidToken(error.Message);
        }
    }
}
