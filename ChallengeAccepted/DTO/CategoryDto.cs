namespace ChallengeAccepted.DTO
{
    public class CategoryTreeDto : CategoryDto
    {
        public IList<CategoryTreeDto> Children { get; set; } = new List<CategoryTreeDto>();
    }
}
