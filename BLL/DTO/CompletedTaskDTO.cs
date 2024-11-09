using DAL.Models;

namespace BLL.DTO
{
    public class CompletedTaskDTO
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public bool IsPassed { get; set; }

        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }
    }
}
