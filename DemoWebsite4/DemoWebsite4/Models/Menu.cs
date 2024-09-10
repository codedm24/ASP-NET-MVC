using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoWebsite4.Models
{
    public class Menu
    {
        public int Id { get; set; }
        [Required, StringLength(60)]
        public string Text { get; set; } = string.Empty;
        [Display(Name = "Price"), DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(10)]
        public string Category { get; set; } = string.Empty;
    }
}
