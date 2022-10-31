namespace ChallengeAccepted.DTO
{
    public class CategoryPostDto
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? ParentId { get; set; }
        public bool Visible { get; set; } = true;
    }
}
