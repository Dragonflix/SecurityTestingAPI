namespace SecurityTestingAPI.Models
{
    public class CompletedTask
    {
        public int Score { get; set; }

        public bool IsPassed { get; set; }

        public TestTask Task { get; set; }

        public Guid TaskId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
