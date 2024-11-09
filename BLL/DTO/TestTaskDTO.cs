using DAL.Models;

namespace BLL.DTO
{
    public class TestTaskDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Prompt { get; set; }

        public Guid ComplexityId { get; set; }

        public Guid TypeId { get; set; }
    }
}
