using Articly.Core.Domain.Account;
using Articly.Core.Domain.Entities;
using Articly.Core.Domain.Enums;
using Articly.Core.Domain.Interfaces;
using Articly.Core.Domain.Model;

namespace Articly.Infrastructure.Security.Auth;

public class AuthenticateService : IAuthenticateService
{
    #region Properties
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;    
    #endregion

    #region Constructors
    public AuthenticateService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;            
    }
    #endregion

    #region Methods

    #region Authenticate
    public async Task<User> Authenticate(string email, string password)
    {
        var user = await userRepository.GetByEmail(email);

        if (user == null || !passwordHasher.VerifyPassword(password, user.Password))
            throw new ApplicationException("Usuário ou senha inválido");
        
        return user;
    }
    #endregion

    #region Logout
    public async Task Logout()
    {
        // await signInManager.SignOutAsync();
    }
    #endregion

    #region Register
    public async Task<Users> Register(string name, string password, string confirmPassword, string email)
    {
        var roles = new List<Role> { Role.Commom };
        var hasEmail = await userRepository.GetByEmail(email) != null;

        if (hasEmail)
            throw new ApplicationException("O e-mail de usuário já existe");
        
        var user = User.Create
        (
            name, 
            password, 
            confirmPassword, 
            email, 
            roles,
            passwordHasher
        );

        var model = await userRepository.Insert(user);
        return model;
    }
    #endregion
    
    #endregion
}