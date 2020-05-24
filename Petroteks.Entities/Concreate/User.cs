using Petroteks.Core.Entities;
using Petroteks.Entities.Abstract;

namespace Petroteks.Entities.Concreate
{
    public class User : EntityBase, IUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TagName { get; set; }
        /// <summary>
        ///  Admin:0
        ///  Editor:1
        ///  Standart:2
        /// </summary>
        public short Role { get; set; } = 2;

    }
}
