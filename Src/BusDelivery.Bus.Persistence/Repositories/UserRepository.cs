using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BusDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusDelivery.Persistence.Repositories;
public class UserRepository : RepositoryBase<User, int>
{
    private readonly ApplicationDbContext context;
    private readonly IConfiguration configuration;
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';
    public UserRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
    {
        this.context = context;
        this.configuration = configuration;
    }

    public string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithmName, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await context.User.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }

    public async Task<User?> FindByIdentityAsync(string identity)
    {
        var user = await context.User.FirstOrDefaultAsync(x => x.Identity == identity);
        return user;
    }


    public bool VerifyPassword(string hashPassword, string inputPassword)
    {
        var elements = hashPassword.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithmName, KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }

    public async Task<JwtSecurityToken> GenerateToken(User user, string roleName)
    {
        try
        {
            var claims = await GenerateClaims(user, roleName);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                audience: configuration["Jwt:Audience"],
                issuer: configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(1),
                signingCredentials: credential
            );
            return token;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task<IEnumerable<Claim>> GenerateClaims(User user, string roleName)
    {

        //Generate claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleName)
        };
        return claims;
    }

    public void Delete(User user)
    {
        user.IsActive = false;
        Update(user);
    }
}
