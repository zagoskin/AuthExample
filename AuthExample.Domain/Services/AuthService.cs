using AuthExample.Domain.Exceptions;
using AuthExample.Domain.Interfaces;
using AuthExample.Domain.Requests;
using AuthExample.Domain.Responses;
using AuthExample.Infrastructure.Interfaces;
using AuthExample.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthExample.Domain.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IConfiguration _configuration;
    public AuthService(IUserRepository userRepository, IRoleRepository roleRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _configuration = configuration;
    }

    public async Task<AuthResponse> Login(LoginUserRequest request)
    {
        var user = await GetUserByUserNameOrEmail(request)
                    ?? throw new NotFoundException("User", request.UserName ?? request.Email!);

        ValidatePassword(user, request);
        return new AuthResponse
        {
            User = new UserResponse(user),
            Token = GenerateToken(user)
        };
    }

    public async Task<UserResponse> Register(RegisterUserRequest request)
    {
        await ValidateRegisterRequest(request);
        var role = await _roleRepository.GetByTypeAsync(request.Role)
                    ?? throw new NotFoundException("Role", request.Role.ToString());

        var user = await CreateUser(request, role);
        return new UserResponse(user);
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private async Task<User> CreateUser(RegisterUserRequest request, Role role)
    {
        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = role
        };
        return await _userRepository.CreateAsync(user);
    }

    private async Task ValidateRegisterRequest(RegisterUserRequest request)
    {
        if (await _userRepository.GetByEmailAsync(request.Email) is not null)        
            throw new BusinessRuleException($"User with Email {request.Email} already exists");
        
        if (await _userRepository.GetByUserNameAsync(request.UserName) is not null)        
            throw new BusinessRuleException($"User with username {request.UserName} already exists");
        
    }

    private async Task<User?> GetUserByUserNameOrEmail(LoginUserRequest request)
    {
        // sempre deveria existir algum validator que nao deixa enviar os dois campos nulos
        // aqui eu asumo que o validator ja foi executado e que pelo menos um dos campos nao é nulo
        if (!string.IsNullOrEmpty(request.UserName))
            return await _userRepository.GetByUserNameAsync(request.UserName);
        return await _userRepository.GetByEmailAsync(request.Email!);
    }

    private void ValidatePassword(User user, LoginUserRequest request)
    {
        if (string.IsNullOrEmpty(request.Password))
            throw new BusinessRuleException("Password is required");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                throw new BusinessRuleException("Invalid password");
        }
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Key"]!);
        var claims = new List<Claim>
        {
            new Claim("uid", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),            
            new Claim(ClaimTypes.Email, user.Email)
        };
        if (user.Role is not null)
            claims.Add(new Claim(ClaimTypes.Role, user.Role!.Name.ToString()));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _configuration["Authentication:Issuer"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
