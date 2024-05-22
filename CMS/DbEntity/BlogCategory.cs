using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CMS.DbEntity
{
    public class BlogCategory
    {
        [Key] //to define a primary key explicitly
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //to mark a property as an identity column
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
       
    }
}
