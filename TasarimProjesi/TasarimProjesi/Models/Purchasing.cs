using System.ComponentModel.DataAnnotations;

namespace TasarimProjesi.Models
{
    public class Purchasing
    {
        [Key]
        public int PurchasingId { get; set; }
        public virtual List<PurchasingItem>? Items { get; set; } = new List<PurchasingItem>();
    }
}
