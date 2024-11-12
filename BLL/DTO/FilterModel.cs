namespace BLL.DTO
{
    public class FilterModel
    {
        public IEnumerable<Guid>? ComplexityIds { get; set; }

        public IEnumerable<Guid>? TaskTypeIdIds { get; set; }

        public string? SortingOption { get; set; }
    }
}
