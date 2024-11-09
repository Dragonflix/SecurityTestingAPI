namespace DAL.Models
{
    public class CompletedTask
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public bool IsPassed { get; set; }

        public TestTask Task { get; set; }

        public Guid TaskId { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
