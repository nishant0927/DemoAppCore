using System.ComponentModel.DataAnnotations;

namespace ASPCoreWebApp.Models
{
    public class ItemViewModel
    {
        public string? ItemCode {  get; set; }
        [Required(ErrorMessage ="Please fill item name.")]
        [MaxLength(50,ErrorMessage = "Item name cannot exceed 50 characters.")]
        public string? ItemName {  get; set; }
        [Required(ErrorMessage ="Please fill Item Description")]
        [MaxLength(50, ErrorMessage = "Item Description cannot exceed 100 characters.")]
        public string? ItemDescription { get; set; }
        [Required(ErrorMessage = "Please fill Item UOM")]
        [MaxLength(10, ErrorMessage = "Item UOM cannot exceed 20 characters.")]
        public string? ItemUOM {  get; set; }
        [Required(ErrorMessage = "Please fill Item Unit Cost")]
        [Range(0,double.MaxValue, ErrorMessage = "Please enter a valid positive number for unit cost.")]
        public decimal ItemUnitCost {  get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; } = Enumerable.Empty<ItemViewModel>();
    }
}
