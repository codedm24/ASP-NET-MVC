using Microsoft.EntityFrameworkCore;

namespace DemoWebsite4.Models
{
    [PrimaryKey("MenuId")]
    public class Menu1
    {
        public int MenuId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool Active {  get; set; }
        public int Order { get; set; }
        public string Type { get; set; } = string.Empty;    
        public DateTime Day { get; set; }
        public int MenuCardId { get; set; }
        public virtual MenuCard? MenuCard { get; set; }  
    }
}
