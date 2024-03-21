using System.ComponentModel.DataAnnotations; 

namespace MyRESTServices.BLL.DTOs
{
    public class ArticleCreateDTO
    {
  
        public int CategoryID { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }
        public bool? IsApproved { get; set; } = false;
        public string? Pic { get; set; }
    }
}
