namespace SecurityTestingAPI.Models
{
    public class TestTask
    {
        public Guid Id { get; set; } 

        public string Title { get; set; }

        public string Description { get; set; }

        public string Prompt { get; set; }

        public Complexity Complexity { get; set; }

        public Guid ComplexityId { get; set; }

        public TaskType Type { get; set; }

        public Guid TypeId { get; set; }
    }
}
