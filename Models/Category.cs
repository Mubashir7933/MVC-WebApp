using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class Category{
 [Key] // Primary key this key is data annotion        
public int CategoryId { get; set; } // Primary key
[Required] // THIS MEANS THAT THE NAME ENTITY IN THE DATABASE CAN'T BE NULL
[DisplayName ("Category Name")]
[MaxLength(20)]
public string Name { get; set; }
[DisplayName ("Category Order")]
[Range(1,20)]
public int  DisplayOrder { get; set; }
    }
}