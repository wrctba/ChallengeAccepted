using System.ComponentModel.DataAnnotations;

namespace ChallengeAccepted.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public string? ParentId { get; set; }
        [Required]
        public bool Visible { get; set; } = true;

        public virtual Category? Parent { get; set; }
    }
}
