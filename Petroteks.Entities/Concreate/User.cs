using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Petroteks.Entities.Concrete
{
   public class User :EntityBase, IUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        /*
         * Admin:0;
         * Editor:1;
         * Standart:2;
         */
        public string Email { get; set; }
        public string Password { get; set; }
        public string TagName { get; set; }
        public short Role { get; set; } = 2;

    }
}
