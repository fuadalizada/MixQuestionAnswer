using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MixQuestionAnswer.BLL.DTOs;
using MixQuestionAnswer.BLL.Services.Abstract;
using MixQuestionAnswer.Common;
using MixQuestionAnswer.DAL;
using MixQuestionAnswer.DAL.Repositories.Abstract;
using MixQuestionAnswer.Domains;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MixQuestionAnswer.BLL.Services.Concrete
{
    public class UserService : Service<UserDTO, User, IUserRepository>, IUserService
    {
       private readonly JwtSettings _jwtSettings;
        public UserService(IUserRepository repository, IMapper mapper, JwtSettings jwtSettings) : base(repository, mapper, "UserService")
        {
            _jwtSettings = jwtSettings;
        }

        public async Task<ActionResult<UserReturnDTO>> Authenticate(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return ActionResult<UserReturnDTO>.Failure("Email is empty");

                var userAll = await GetAllAsync();
                if (userAll.IsSucceed)
                {
                    var user = userAll.Data.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());  // x => x.Email == email);
                    if (user == null)
                        return ActionResult<UserReturnDTO>.Failure("User does not exist");

                    if (!VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
                        return ActionResult<UserReturnDTO>.Failure("Password is not correct");

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName+" "+user.LastName),
                    new Claim("Id", user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    return ActionResult<UserReturnDTO>.Succeed(new UserReturnDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Token = tokenString
                    });
                }
                return ActionResult<UserReturnDTO>.Failure("Problem occured");
            }
            catch (Exception ex)
            {
                return ActionResult<UserReturnDTO>.Failure(ex.Message);
            }
        }
        public async Task<ActionResult> AddRole(UserDTO userDTO ,Guid roleId)
        {
            try
            {
                var userRoles = new UserRoleDTO
                {
                    UserId = userDTO.Id,
                    RoleId = roleId
                };

                userDTO.UserRoles.Add(userRoles);
                var added = await SaveAsync(userDTO);

                if (added.IsSucceed)
                    return ActionResult.Succeed();
                return ActionResult.Failure("Problem occured");
            }
            catch (Exception ex)
            {
                return ActionResult.Failure(ex.Message);
            }
        }

        public async Task<ActionResult> ChangePassword(Guid Id, string password)
        {
            try
            {
                var user = await GetByIdAsync(Id);
                if (user.IsSucceed)
                {
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        byte[] passwordHash, passwordSalt;
                        CreatePasswordHash(password, out passwordHash, out passwordSalt);
                        user.Data.PasswordHash = Convert.ToBase64String(passwordHash);
                        user.Data.PasswordSalt = Convert.ToBase64String(passwordSalt);

                      var changed = await SaveAsync(user.Data);
                        if (changed.IsSucceed)
                            return ActionResult.Succeed();

                    }
                    return ActionResult.Failure("Password is empty");
                }
                return ActionResult.Failure("");
            }
            catch (Exception ex)
            {
                return ActionResult.Failure(ex.Message);
            }
        }

        #region helper
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        
        #endregion

    }
}
