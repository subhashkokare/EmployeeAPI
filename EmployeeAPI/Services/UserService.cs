using EmployeeAPI.Context;
using EmployeeAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);

        bool Postuser(User user);

    }
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", UserName = "test", Password = "test" }
        //};

        private readonly AppSettings _appSettings;
        readonly EmployeeContext _employeeContext;
        public UserService(IOptions<AppSettings> appSettings, EmployeeContext employeeContext)
        {
            _appSettings = appSettings.Value;
            _employeeContext = employeeContext;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _employeeContext.User.Where(x => x.UserName == model.Username && x.Password == model.Password).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _employeeContext.User.ToList();
        }

        public User GetById(int id)
        {
            return _employeeContext.User.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool Postuser(User user)
        {
            _employeeContext.User.Add(user);
            _employeeContext.SaveChanges();
            return true;
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    }
