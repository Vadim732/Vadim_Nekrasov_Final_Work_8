using System.ComponentModel.DataAnnotations;

namespace Forum.Models;

public class Theme
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    [StringLength(200, ErrorMessage = "Описание не может превышать 200 символов!")]
    public string Description { get; set; }
    public DateTime DateOfCreation { get; set; }
    
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    public ICollection<Message>? Messages { get; set; }
}