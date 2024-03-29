﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HeroicBrawlServer.Data.Entities;
using HeroicBrawlServer.Data.Repositories.Abstractions;
using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Configuration;
using HeroicBrawlServer.Services.Models.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HeroicBrawlServer.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IOptions<SecurityOptions> _securityOptions;

        public UserService(IUserRepository userRepository,
                           IOptions<SecurityOptions> securityOptions)
        {
            _userRepository = userRepository;
            _securityOptions = securityOptions;
        }

        public async Task<(User, string)> Login(LoginParameter loginParameter)
        {
            var user = await _userRepository.GetByEmailAsync(loginParameter.Email);

            if (user == null)
            {
                throw new Exception($"User not found with email {loginParameter.Email}");
            }

            CheckPassword(loginParameter.Password, user);

            var token = await Task.Run(() => GenerateToken(user));

            return (user, token);
        }

        public async Task<(User, string)> SignUpAsync(SignUpParameter signUpParameter)
        {
            var existingUser = await _userRepository.GetByEmailAsync(signUpParameter.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already used");
            }

            var salt = GenerateSalt();
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = signUpParameter.Email,
                Pseudo = signUpParameter.Pseudo,
                PasswordSalt = salt.ToString(),
                PasswordHash = HashUsingPbkdf2(signUpParameter.Password, salt.ToString())
            };
            user.Init(user.Id);

            var token = await Task.Run(() => GenerateToken(user));

            var result = await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return (result, token);
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<User> ChangePseudo(ChangePseudoParameter changePseudoParameter, Guid userId)
        {
            var user = await GetByIdAsync(userId);

            user.Pseudo = changePseudoParameter.Pseudo;
            user.Update(userId);

            var result = await _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return result;
        }

        public async Task<User> ChangePassword(ChangePasswordParameter changePasswordParameter, Guid userId)
        {
            var user = await GetByIdAsync(userId);

            CheckPassword(changePasswordParameter.OldPassword, user);

            user.PasswordHash = HashUsingPbkdf2(changePasswordParameter.NewPassword, user.PasswordSalt.ToString());

            var result = await _userRepository.Update(user);
            await _userRepository.SaveAsync();

            return result;
        }

        public async Task<ICollection<User>> GetUsersByIds(ICollection<Guid> userIds)
        {
            return await _userRepository.GetByIds(userIds);
        }

        #region private functions
        private void CheckPassword(string password, User user)
        {
            var passwordHash = HashUsingPbkdf2(password, user.PasswordSalt);

            if (user.PasswordHash != passwordHash)
            {
                throw new Exception($"Incorrect password");
            }
        }

        /// <summary>
        ///     Hash a password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string HashUsingPbkdf2(string password, string salt)
        {
            using var bytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);
            return hash;
        }

        /// <summary>
        ///     Generate a random base64 string for salt
        /// </summary>
        /// <returns></returns>
        private string GenerateSalt()
        {
            var randomBytes = new byte[64];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityOptions.Value.Secret));

            var permClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(_securityOptions.Value.Issuer,  
                                             _securityOptions.Value.Audience,   
                                             permClaims,
                                             signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
