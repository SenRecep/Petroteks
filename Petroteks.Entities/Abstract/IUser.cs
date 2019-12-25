
namespace Petroteks.Entities.Abstract
{
    public interface IUser
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string TagName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        short Role { get; set; }
    }
}
