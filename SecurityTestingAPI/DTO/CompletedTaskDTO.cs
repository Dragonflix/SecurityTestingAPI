using SecurityTestingAPI.Models;

namespace SecurityTestingAPI.DTO
{
    public class CompletedTaskDTO
    {
        public int Score { get; set; }

        public bool IsPassed { get; set; }

        public Guid TaskId { get; set; }

        public Guid UserId { get; set; }
    }
}
