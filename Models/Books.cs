using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace JokeMVCApp.Models
{
    public class Books{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "Unknown";
        
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be 1 and 100 only!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }

    public class BooksSearchViewModel{

        public string? Name { get; set; }
        
        public IEnumerable<Books> Results { get; set; }
    }
}