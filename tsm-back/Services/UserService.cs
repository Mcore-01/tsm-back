using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using tsm_back.Models;
using tsm_back.Repositories;
using tsm_back.ViewModels;

namespace tsm_back.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserDTO> Login(string login, string password)
        {
            User? user = await _repository.GetUser(login) ?? throw new Exception("Такой логин не найден");

            if (user.Password != GetHashPassword(password))
                throw new Exception("Неправильный пароль!");

            return await GetUserDTO(user);
        }
        public async Task<UserDTO> Register(string nickname, string login, string password)
        {
            User user = new() { Nickname = nickname, Login = login, Password = GetHashPassword(password) };

            await _repository.CreateUser(user);

            return await GetUserDTO(user);
        }
        public async Task UpdateUserName(int userID, string name)
        {
            await _repository.UpdateUserName(userID, name);
        }


        private Task<UserDTO> GetUserDTO(User user)
        {
            return Task.Run(() =>
            {
                return new UserDTO
                {
                    UserID = user.Id,
                    UserName = user.Nickname,
                    Token = CreateJWT(user.Nickname)
                };
            });
        }
        private string CreateJWT(string username)
        {
            var claims = new List<Claim> { new(ClaimTypes.Name, username) };
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        //чтобы было хоть какое то хеширование пароля
        private string GetHashPassword(string password)
        {
            byte[] bytesPassword = Encoding.ASCII.GetBytes(password);
            byte[] hashPassword = MD5.HashData(bytesPassword);

            return string.Join(string.Empty, hashPassword.Select(e => e.ToString("X2")));
        }
    }
}
