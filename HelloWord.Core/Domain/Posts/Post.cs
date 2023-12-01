using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloWord.Core.Domain.Posts;

public class Post: EntityAudit
{
    public Post()
    {
        
    }
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Content { get; set; }
}