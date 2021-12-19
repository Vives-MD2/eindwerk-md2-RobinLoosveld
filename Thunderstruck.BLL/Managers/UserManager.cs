using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Thunderstruck.DAL.DBs;
using Thunderstruck.DOMAIN.Contracts;
using Thunderstruck.DOMAIN.Helpers;
using Thunderstruck.DOMAIN.Models;

namespace Thunderstruck.BLL.Managers
{
    public class UserManager : IUser
    {
        private readonly UserDb _db = new UserDb();

        public async Task<User> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return new User()
                {
                    Tex = new ThunderstruckException("User Id is lower than 1, unusable.", ExceptionTypes.Warning)
                };
            }
            var user = await _db.GetByIdAsync(id);
            return user ?? new User()
            {
                Tex = new ThunderstruckException("No user found.", ExceptionTypes.Warning)
            };
        }

        public async Task<IEnumerable<User>> GetAsync(int skip, int take)
        {
            return await _db.GetAsync(skip, take);
        }

        public async Task<User> CreateAsync(User entity)
        {
            if(!IsValidEmail(entity.Email))
            {
                entity.Tex = new ThunderstruckException("Invalid Email", ExceptionTypes.Warning);
            }
            return await _db.CreateAsync(entity);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            if (!IsValidEmail(entity.Email))
            {
                entity.Tex = new ThunderstruckException("Invalid Email", ExceptionTypes.Warning);
            }
            return await _db.UpdateAsync(entity);
        }

        public async Task<User> DeleteAsync(User entity)
        {
            return await _db.DeleteAsync(entity);
        }
        // source: https://stackoverflow.com/a/48476318/3701072
        public bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }
    }
}