using Ex_Revision_Middleware.Core.Models;
using Ex_Revision_Middleware.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ex_Revision_Middleware.Business
{
    public class UserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
        //public async Task<User> CreateAsync(User user)
        //{
        //var length = 1000;
        //var salt = this.GenerateSalt(length);
        //var encodedPassword = Encoding.ASCII.GetBytes(user.Password)
        /*byte[] salt = new byte[128 / 8];
        using (var rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetNonZeroBytes(salt);
        }
        user.Salt = Convert.ToBase64String(salt);
        user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: user.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
            ));

        return await this.userRepository.CreateAsync(user);*/
        //}

        /*public bool Login(Credentials credentials,User user)
        {
            var ps = credentials.Password;
            ps = CreerHash(user);
            if (user.Password == ps)
            {
                Console.WriteLine("Ils sont identiques");
            }
            else
            {
                Console.WriteLine("Ils sont =/=");
            }

            return true;

        }*/

        /*public string CreerHash(User user)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            user.Salt = Convert.ToBase64String(salt);
            user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            return user.Password;
        }*/

        public async Task<User> CreateAsync(User user)
        {
            var length = 1000;
            var salt = GenerateSalt(length);
            var encodedPassword = Encoding.ASCII.GetBytes(user.Password);
            var hash = GenerateHash(encodedPassword, salt, 10000, length);

            user.Salt = Convert.ToBase64String(salt);
            user.Password = Convert.ToBase64String(hash);

            return await this._userRepository.CreateAsync(user);
        }

        public async Task<(UserData, IEnumerable<Role> role)> Login(Credentials credentials)
        {
            var user = await this._userRepository.GetUserByEmailAsync(credentials.Email);

            if(user == null)
            {
                return (null, null);
            }

            var roleList = await this._userRepository.GetRoleByUserIdAsync(user.Id);

            var length = 1000;
            var encodedPassword = Encoding.ASCII.GetBytes(credentials.Password);
            var hash = GenerateHash(encodedPassword, Convert.FromBase64String(user.Salt), 10000, length);
            var hashToCompare = Convert.ToBase64String(hash);

            if (user.Password != hashToCompare)
            {
                return (null,null);
            }

            var userData = new UserData
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return (userData, roleList);
        }

        private static byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        private static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

    }
}
