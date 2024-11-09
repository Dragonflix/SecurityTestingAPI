using DAL.Models;

namespace BLL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<Guid> Roles { get; set; }
    }
}
