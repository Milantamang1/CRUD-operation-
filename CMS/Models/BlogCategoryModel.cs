using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class BlogCategoryModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
