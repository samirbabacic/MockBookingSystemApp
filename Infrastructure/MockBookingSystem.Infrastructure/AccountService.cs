using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MockBookingSystem.Application.Objects.User;
using MockBookingSystem.Core;
using MockBookingSystem.Entities;
using MockBookingSystem.Objects.User;
using MockBookingSystem.ServiceLayer.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MockBookingSystem.Infrastructure
{
    public class AccountService : IAccountService
    {
        private readonly string _tokenKey;
        private readonly IDAL _dal;
        public AccountService(IOptions<ClientSettings> clientSettings, IDAL dal)
        {
            _tokenKey = clientSettings.Value.JwtTokenKey;
            _dal = dal;
        }

        public TokenOutput Register(UserModel userModel)
        {
            CreatePasswordHash(userModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            _dal.Insert(new User
            {
                Username = userModel.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

            var token = CreateToken(userModel);

            return new TokenOutput
            {
                Token = token
            }; 
        }

        public TokenOutput Login(UserModel model)
        {
            var user = _dal.FirstOrDefault<User>(x => x.Username == model.Username);

            if (user == null)
                throw new AuthenticationException("User with that username not found");

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                throw new AuthenticationException("Incorrect password");

            var token = CreateToken(model);

            return new TokenOutput
            {
                Token = token
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_tokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}