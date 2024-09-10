using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoWebsite4.Models
{
    [PrimaryKey("MenuCardId")]
    public class MenuCard
    {
        public int MenuCardId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        public bool Active { get; set; }
        public int Order { get; set; }
        public virtual List<Menu1> Menus { get; set; } = new List<Menu1>();
    }
}
