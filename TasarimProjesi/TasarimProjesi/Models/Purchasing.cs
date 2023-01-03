using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasarimProjesi.Models
{
    public class Purchasing
    {
        [Key]
        public int PurchasingId { get; set; }
        public virtual List<PurchasingItem>? Items { get; set; } = new List<PurchasingItem>();
		public string? User { get; set; }

	}
}
